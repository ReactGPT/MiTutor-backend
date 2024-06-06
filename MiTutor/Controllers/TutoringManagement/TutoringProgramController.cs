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
        [HttpPost("/crearEditarProgramaDeTutoria")]
        public async Task<IActionResult> CrearEditarProgramaTutoria([FromBody] TutoringProgram TutoringProgram)
        {
            try
            {
                await _TutoringProgramServices.CrearEditarProgramaDeTutoria(TutoringProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(new{ sucess=false,message=ex.Message});
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("listarProgramasDeTutoria")]   // Para Verificar el Endpoint en el Controlador
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

        [HttpGet("/listarProgramasDeTutoriaPorTutor/{tutorId}")]
        public async Task<IActionResult> ListarProgramasDeTutoriaPorTutor(int tutorId)
        {
            try
            {
                
                var programas = await _TutoringProgramServices.ListarProgramasDeTutoriaPorTutor(tutorId);

                
                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/listarProgramasDeTutoriaPorTipoUsuario/{userAccountTypeId}")]
        public async Task<IActionResult> ListarProgramasDeTutoriaPorTipoUsuario(int userAccountTypeId)
        {
            try
            {
                var programas = await _TutoringProgramServices.ListarProgramasDeTutoriaPorTipoUsuario(userAccountTypeId);

                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarProgramasDeTutoriaPorAlumno/{studentId}")]
        public async Task<IActionResult> ListarProgramasDeTutoriaPorAlumno(int studentId)
        {
            try
            {

                var programas = await _TutoringProgramServices.ListarProgramasDeTutoriaPorAlumno(studentId);


                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
