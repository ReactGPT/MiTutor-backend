using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.GestionUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentService _estudianteServices;

        public StudentController(ILogger<StudentController> logger, StudentService estudianteService)
        {
            _logger = logger;
            _estudianteServices = estudianteService;

            _logger.LogInformation("Se inicio el constructor");
        }

        [HttpPost("/crearEstudiante")]
        public async Task<IActionResult> CrearEstudiante([FromBody] Student estudiante)
        {
            try
            {
                await _estudianteServices.CrearEstudiante(estudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }

        [HttpGet("/listarEstudiantes")]
        public async Task<IActionResult> ListarEstudiantes()
        {
            List<Student> students;
            try
            {
                students = await _estudianteServices.ListarEstudiantes();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = students });
        }

        [HttpGet("/listarEstudiantesTodo")]
        public async Task<IActionResult> ListarEstudiantesTodo()
        {
            List<StudentTodo> students;
            try
            {
                students = await _estudianteServices.ListarEstudiantesTodo();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = students });
        }

        [HttpGet("/listarEstudiantesPorProgramaDeTutoria/{tutoringProgramId}")]
        public async Task<IActionResult> ListarProgramasDeTutoriaPorTutor(int tutoringProgramId)
        {
            try
            {

                var estudiantes = await _estudianteServices.ListarEstudiantesByTutoringProgram(tutoringProgramId);


                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/seleccionarEstudiantePorId/{studentId}")]
        public async Task<IActionResult> SeleccionarDatosEstudiantesById(int studentId)
        {
            try
            {

                var estudiantes = await _estudianteServices.SeleccionarDatosEstudiantesById(studentId);


                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
