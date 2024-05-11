using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Services;

namespace MiTutor.Controllers
{
    [Route("/action_plan")]
    [ApiController]
    public class ActionPlanController : ControllerBase
    {

        private readonly ILogger<ActionPlanController> _logger;
        private readonly ActionPlanService _actionPlanService;

        public ActionPlanController(ILogger<ActionPlanController> logger, ActionPlanService actionPlanService)
        {
            _logger = logger;
            _actionPlanService = actionPlanService;
        }

        [HttpGet("/listarActionPlans")]
        public async Task<IActionResult> ListarActionPlans([FromQuery] int StudentProgramId, [FromQuery] int TutorId)
        {
            List<ActionPlan> actionPlans;
            try
            {
                actionPlans = await _actionPlanService.ListarActionPlans(StudentProgramId, TutorId);
                return Ok(new { success = true, data = actionPlans });
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en controller", ex);
                //return BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpGet("/listarActionPlansPorId")]
        public async Task<IActionResult> ListarActionPlansPorId([FromQuery] int ActionPlanId)
        {
            List<ActionPlan> actionPlans;
            try
            {
                actionPlans = await _actionPlanService.ListarActionPlansPorId(ActionPlanId);
                return Ok(new { success = true, data = actionPlans });
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en controller", ex);
                //return BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpPost("/crearActionPlan")]
        public async Task<IActionResult> CrearActionPlan([FromBody] ActionPlan actionPlan)
        {
            try
            {
                await _actionPlanService.CrearActionPlan(actionPlan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }


        [HttpPut("/actualizarActionPlan")]
        public async Task<IActionResult> ActualizarActionPlan([FromBody] ActionPlan actionPlan)
        {
            try
            {
                await _actionPlanService.ActualizarPlan(actionPlan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente datos de plan de acción." });
        }


        [HttpPut("/eliminarActionPlan")]
        public async Task<IActionResult> EliminarActionPlan([FromQuery] int actionPlanId)
        {
            try
            {
                await _actionPlanService.EliminarActionPlan(actionPlanId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó satisfactoriamente." });
        }
    }
}
