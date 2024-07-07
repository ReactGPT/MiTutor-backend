using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using MiTutor.Models.GestionUsuarios;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.TutoringManagement;
using MiTutor.Services.UserManagement;
using MiTutor.Models.GestionUsuarios;
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


        [HttpGet("/listarEstudiantesPorIdProgramaTutoria")]
        public async Task<IActionResult> ListarEstudiantesPorIdProgramaTutoria(int idProgramaTutoria)
        {
            List<StudentTutoria> students;
            try
            {
                students = await _estudianteServices.ListarEstudiantesPorIdProgramaTutoria(idProgramaTutoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = students });
        }

        [HttpPost("/listarEstudiantesPorId")]
        public async Task<IActionResult> ListarEstudiantesPorId([FromBody] List<StudentIdVerified> studentsVerified)
        {
            List<StudentIdVerified> students;
            try
            {
                students = await _estudianteServices.ListarEstudiantesPorId(studentsVerified);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = students });
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

        [HttpGet("/listarEstudiantePorIdFacultad/{idFaculty}")]
        public async Task<IActionResult> ListarEstudiantesPorIdFacultad(int idFaculty)
        {
            try
            {
                var estudiantes = await _estudianteServices.ListarEstudiantesPorIdFacultad(idFaculty);
                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/listarEstudiantePorIdEspecialidad/{idSpecialty}")]
        public async Task<IActionResult> ListarEstudiantesPorIdEspecialidad(int idSpecialty)
        {
            try
            {
                var estudiantes = await _estudianteServices.ListarEstudiantesPorIdEspecialidad(idSpecialty);
                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/listarEstudiantesPorEspecialidad/{idEspecialidad}")]
        public async Task<IActionResult> ListarEstudiantesPorEspecialidad(int idEspecialidad)
        {
            try
            {

                var estudiantes = await _estudianteServices.ListarEstudiantesPorEspecialidad(idEspecialidad);

                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarEstudiantesPorFacultad/{idFacultad}")]
        public async Task<IActionResult> ListarEstudiantesPorFacultad(int idFacultad)
        {
            try
            {

                var estudiantes = await _estudianteServices.ListarEstudiantesPorFacultad(idFacultad);

                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
