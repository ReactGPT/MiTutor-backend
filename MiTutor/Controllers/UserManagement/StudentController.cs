using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.TutoringManagement;
using MiTutor.Services.UserManagement;

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
        public async Task<IActionResult> CrearEstudiante([FromBody] StudentTodo estudiante)
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

        [HttpGet("/listarAlumnosConCantidadDeProgramas")]
        public async Task<IActionResult> ListarAlumnosConCantidadDeProgramas()
        {
            List<StudentContadorProgramasAcademicos> alumnos;
            try
            {
                alumnos = await _estudianteServices.ListarAlumnosConCantidadDeProgramas();
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            return Ok(new { success = true, data = alumnos });
        }

        [HttpGet("/listarCantidadAppointmentsStudent")]
        public async Task<IActionResult> ListarCantidadAppointmentsStudent()
        {
            List<ListarCantidadAppointmentsStudent> appointments;
            try
            {
                appointments = await _estudianteServices.ListarCantidadAppointmentsStudent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            return Ok(new { success = true, data = appointments });
        }

        [HttpGet("/listarProgramaFechaStudent/{studentId}")]
        public async Task<IActionResult> ListarProgramaFechaStudent(int studentId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                List<StudentTutoringProgram> programs = await _estudianteServices.ListarProgramaFechaStudent(studentId, startDate, endDate);
                return Ok(new { success = true, data = programs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los programas por fecha: " + ex.Message });
            }
        }

        [HttpGet("/listarAppointmentPorFechaStudent/{studentId}")]
        public async Task<IActionResult> ListarAppointmentPorFechaStudent(int studentId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                List<StudentAppointment> appointments = await _estudianteServices.ListarAppointmentPorFechaStudent(studentId, startDate, endDate);
                return Ok(new { success = true, data = appointments });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar las citas por fecha: " + ex.Message });
            }
        }

        [HttpGet("/listarProgramaVirtualFaceStudent/{studentId}")]
        public async Task<IActionResult> ListarProgramaVirtualFaceStudent(int studentId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                var programasVirtualFace = await _estudianteServices.ListarProgramaVirtualFaceStudent(studentId, startDate, endDate);
                return Ok(new { success = true, data = programasVirtualFace });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los programas virtuales y presenciales: " + ex.Message });
            }
        }
        
        [HttpGet("/listarEstudiantePorIdCita/{idAppointmen}")]
        public async Task<IActionResult> ListarEstudiantesPorIdCita(int idAppointmen)
        {
            try
            {

                var estudiantes = await _estudianteServices.ListarEstudiantesPorIdCita(idAppointmen);


                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
