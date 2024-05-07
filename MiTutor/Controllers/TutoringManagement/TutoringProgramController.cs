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
    public class TutoringProgramController : ControllerBase
    {
        private readonly ILogger<TutoringProgramController> _logger;
        private readonly TutoringProgramService _TutoringProgramServices;

        public TutoringProgramController(ILogger<TutoringProgramController> logger, TutoringProgramService TutoringProgramService)
        {
            _logger = logger;
            _TutoringProgramServices = TutoringProgramService;
        }

        [HttpPost("/crearProgramaDeTutoria")]
        public async Task<IActionResult> CrearProgramaDeTutoria([FromBody] TutoringProgram TutoringProgram)
        {
            try
            {
                await _TutoringProgramServices.CrearProgramaDeTutoria(TutoringProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("/listarProgramasDeTutoria")]
        public async Task<IActionResult> ListarProgramasDeTutoria()
        {
            List<TutoringProgram> faculties;
            try
            {
                faculties = await _TutoringProgramServices.ListarProgramasDeTutoria();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = faculties });
        }
    }
}
