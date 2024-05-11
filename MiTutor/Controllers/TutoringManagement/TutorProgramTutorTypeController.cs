using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Controllers.UniversityUnitManagement;
using MiTutor.Models.TutoringManagement;
using MiTutor.Models.UniversityUnitManagement;
using MiTutor.Services.TutoringManagement;
using MiTutor.Services.UniversityUnitManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorProgramTutorTypeController : ControllerBase
    {

        private readonly ILogger<TutorProgramTutorTypeController> _logger;
        private readonly TutorProgramTutorTypeService _TutorProgramTutorTypeServices;

        public TutorProgramTutorTypeController(ILogger<TutorProgramTutorTypeController> logger, TutorProgramTutorTypeService TutorProgramTutorTypeService)
        {
            _logger = logger;
            _TutorProgramTutorTypeServices = TutorProgramTutorTypeService;
        }

        [HttpPost("/crearProgramaTutorTipo")]
        public async Task<IActionResult> CrearTutorProgramTutorType([FromBody] TutorProgramTutorType TutorProgramTutorType)
        {
            try
            {
                await _TutorProgramTutorTypeServices.CrearTutorProgramTutorType(TutorProgramTutorType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("/listarProgramaTutorTipo")]
        public async Task<IActionResult> ListarTutorProgramTutorType()
        {
            List<TutorProgramTutorType> faculties;
            try
            {
                faculties = await _TutorProgramTutorTypeServices.ListarTutorProgramTutorType();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = faculties });
        }
    }
}
