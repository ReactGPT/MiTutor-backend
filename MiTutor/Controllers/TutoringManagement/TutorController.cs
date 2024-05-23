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
    public class TutorController : ControllerBase
    {
        private readonly ILogger<TutorController> _logger;
        private readonly TutorService _tutorServices;

        public TutorController(ILogger<TutorController> logger, TutorService tutorService)
        {
            _logger = logger;
            _tutorServices = tutorService;
        }

        [HttpPost("/crearTutor")]
        public async Task<IActionResult> CrearTutor([FromBody] Tutor tutor)
        {
            try
            {
                await _tutorServices.CrearTutor(tutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("/listarTutores")]
        public async Task<IActionResult> ListarTutores()
        {
            List<Tutor> faculties;
            try
            {
                faculties = await _tutorServices.ListarTutores();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = faculties });
        }

        [HttpGet("/listarTutoresTipo")]
        public async Task<IActionResult> ListarTutoresTipos()
        {
            try
            {
                var tutores = await _tutorServices.ListarTutoresTipo();
                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los tutores: " + ex.Message });
            }
        }
    }
}
