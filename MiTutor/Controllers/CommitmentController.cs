using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Services;

namespace MiTutor.Controllers
{
    [Route("/Commitment")]
    [ApiController]
    public class CommitmentController : ControllerBase
    {
        private readonly ILogger<CommitmentController> _logger;
        private readonly CommitmentService _commitmentService;

        public CommitmentController(ILogger<CommitmentController> logger, CommitmentService commitmentService)
        {
            _logger = logger;
            _commitmentService = commitmentService;
        }
        [HttpGet("/listarCommitment")]
        public async Task<IActionResult> ListarCommitment([FromQuery] int IdPlanAction)
        {
            List<Commitment> commitments;
            try
            {
                commitments = await _commitmentService.ListarCommitmentPorIdPlanAction(IdPlanAction);
                return Ok(new { success = true, data = commitments });
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en controller", ex);
                //return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("/crearCommitment")]
        public async Task<IActionResult> CrearCommitment([FromBody] Commitment commitment)
        {
            try
            {
                await _commitmentService.CrearCommitment(commitment.ActionPlanId, commitment.Description);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertó  satisfactoriamente el compromiso." });
        }

        [HttpPut("/actualizarCommitment")]
        public async Task<IActionResult> ActualizarCommitment([FromBody] Commitment commitment)
        {
            try
            {
                await _commitmentService.ActualizarCommitment(commitment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente." });
        }

        [HttpPut("/eliminarCommitment")]
        public async Task<IActionResult> EliminarCommitment([FromQuery] int commitmentId)
        {
            try
            {
                await _commitmentService.EliminarCommitment(commitmentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó satisfactoriamente." });
        }

    }
}
