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

        [HttpPost("/crearTutorBatch")]
        public async Task<IActionResult> CrearTutoresBatch([FromBody] List<Tutor> tutores)
        {
            try
            {
                await _tutorServices.CrearTutoresBatch(tutores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente " + tutores.Count.ToString() + " tutores"});
        }

        [HttpGet("/listarTutores")]
        public async Task<IActionResult> ListarTutores(int idProgramaTutoria=-1)
        {
            List<Tutor> tutores;
            try
            {
                tutores = await _tutorServices.ListarTutores(idProgramaTutoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = tutores });
        }

        [HttpGet("listarTutoresPorNombreApellido/{nombreApellido?}")]
        public async Task<IActionResult> ListarTutoresPorNombreApellido(string nombreApellido = "")
        {
            try
            {
                List<Tutor> tutores;

                if (string.IsNullOrWhiteSpace(nombreApellido))
                {
                    tutores = await _tutorServices.ListarTutores(-1); // Llama al método para listar todos los tutores si no hay query
                }
                else
                {
                    tutores = await _tutorServices.ListarTutoresPorNombreApellido(nombreApellido);
                }

                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los tutores: " + ex.Message });
            }
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


        [HttpGet("/seleccionarTutorPorId/{tutorId}")]
        public async Task<IActionResult> SeleccionarTutorxID(int tutorId)
        {
            try
            {

                var estudiantes = await _tutorServices.SeleccionarTutorxID(tutorId);


                return Ok(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarTutoresPorPrograma/{idProgram}")]
        public async Task<IActionResult> ListarTutoresPorProgramas(int idProgram)
        {
            try
            {
                List<Tutor> tutores = await _tutorServices.ListarTutoresPorPrograma(idProgram);
                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los tutores por programa: " + ex.Message });
            }
        }

        [HttpGet("/listarTutoresPorProgramaPorAlumno/{idProgram}/{studentId}")]
        public async Task<IActionResult> ListarTutoresPorProgramasPorAlumno(int idProgram,int studentId)
        {
            try
            {
                string estado = null;

                List<TutorXtutoringProgramXalumno> tutores = await _tutorServices.ListarTutoresPorTutoriaPorAlumno(idProgram, studentId);
                if (tutores.Count == 0)
                {
                    //no hay tutor
                    estado = "SIN_TUTOR";
                }
                else {
                    if (tutores[0].State == "ASIGNADO") {
                        //tutor asignado
                        estado = "TUTOR_ASIGNADO";
                    }
                    else if(tutores[0].State == "SOLICITADO"){
                        //tutor solicitado
                        estado = "SOLICITUD_PENDIENTE";
                    }
                    else if (tutores[0].State == "RECHAZADO")
                    {
                        //tutor solicitado
                        estado = "SIN_TUTOR";
                    }
                }

                return Ok(new { success = true, data = new { estado = estado, tutores = tutores } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los tutores por programa por alumno: " + ex.Message });
            }
        }

        [HttpGet("/listarTutoresPorProgramaVariable/{idProgram}")]
        public async Task<IActionResult> ListarTutoresPorProgramasVariable(int idProgram)
        {
            try
            {
                List<TutorXtutoringProgramXalumno> tutores = await _tutorServices.ListarTutoresPorTutoriaVariable(idProgram);
                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los tutores por programa variable: " + ex.Message });
            }
        }

        [HttpGet("/listarTutoresConCantidadDeProgramas")]
        public async Task<IActionResult> ListarTutoresConCantidadDeProgramas()
        {
            List<TutorContadorProgramasAcademicos> tutores;
            try
            {
                tutores = await _tutorServices.ListarTutoresConCantidadDeProgramas();
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            return Ok(new { success = true, data = tutores });
        }

        [HttpGet("/listarCantidadAppointments")]
        public async Task<IActionResult> ListarCantidadAppointments()
        {
            List<ListarCantidadAppointment> appointments;
            try
            {
                appointments = await _tutorServices.ListarCantidadAppointments();
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            return Ok(new { success = true, data = appointments });
        }

        [HttpGet("/listarProgramaFecha/{tutorId}")]
        public async Task<IActionResult> ListarProgramaFecha(int tutorId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                List<TutorProgram> programs = await _tutorServices.ListarProgramaFecha(tutorId, startDate, endDate);
                return Ok(new { success = true, data = programs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los programas por fecha: " + ex.Message });
            }
        }
        [HttpGet("/listarAppointmentPorFecha/{tutorId}")]
        public async Task<IActionResult> ListarAppointmentPorFecha(int tutorId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                List<TutorAppointment> appointments = await _tutorServices.ListarAppointmentPorFecha(tutorId, startDate, endDate);
                return Ok(new { success = true, data = appointments });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar las citas por fecha: " + ex.Message });
            }
        }
        [HttpGet("/listarProgramaVirtualFace/{tutorId}")]
        public async Task<IActionResult> ListarProgramaVirtualFace(int tutorId, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null)
        {
            try
            {
                var programasVirtualFace = await _tutorServices.ListarProgramaVirtualFace(tutorId, startDate, endDate);
                return Ok(new { success = true, data = programasVirtualFace });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los programas virtuales y presenciales: " + ex.Message });
            }
        }

        [HttpGet("/listarTutoresPorIdFacultad/{idFaculty}")]
        public async Task<IActionResult> ListarTutoresPorIdFacultad(int idFaculty)
        {
            try
            {
                var tutores = await _tutorServices.ListarTutoresPorIdFacultad(idFaculty);
                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarTutoresPorIdEspecialidad/{idSpeciality}")]
        public async Task<IActionResult> ListarTutoresPorIdEspecialidad(int idSpeciality)
        {
            try
            {
                var tutores = await _tutorServices.ListarTutoresPorIdEspecialidad(idSpeciality);
                return Ok(new { success = true, data = tutores });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //UPDATE
        [HttpGet("/listarAlumnosPorIdTutor/{tutorId}")]
        public async Task<IActionResult> ListarAlumnosPorIdTutor(int tutorId)
        {
            try
            {
                var alumnos = await _tutorServices.ListarAlumnosPorIdTutor(tutorId);
                return Ok(new { success = true, data = alumnos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/contarEstudiantesPorIdTutor/{tutorId}")]
        public async Task<IActionResult> ContarEstudiantesPorIdTutor(int tutorId)
        {
            try
            {
                var studentCount = await _tutorServices.ContarEstudiantesPorIdTutor(tutorId);
                return Ok(new { success = true, data = studentCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarProgramasDeTutoriaPorIdTutor/{tutorId}")]
        public async Task<IActionResult> ListarTodosProgramasDeTutoriaPorIdTutor(int tutorId)
        {
            try
            {
                var programas = await _tutorServices.ListarTodosProgramasDeTutoriaPorIdTutor(tutorId);
                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/listarCitasPorIdTutor/{tutorId}")]
        public async Task<IActionResult> ListarTodasCitasPorIdTutor(int tutorId)
        {
            try
            {
                var citas = await _tutorServices.ListarTodasCitasPorIdTutor(tutorId);
                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/listarProgramasPorEstudianteYTutor/{tutorId}/{studentId}")]
        public async Task<IActionResult> ListarProgramasPorEstudianteYTutor(int tutorId, int studentId)
        {
            try
            {
                var programas = await _tutorServices.ListarProgramasPorEstudianteYTutor(tutorId, studentId);
                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarCitasPorEstudianteYTutor/{tutorId}/{studentId}")]
        public async Task<IActionResult> ListarCitasPorEstudianteYTutor(int tutorId, int studentId)
        {
            try
            {
                var citas = await _tutorServices.ListarCitasPorEstudianteYTutor(tutorId, studentId);
                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/contarCitasPorEstudianteYTutor/{tutorId}/{studentId}")]
        public async Task<IActionResult> ContarCitasPorEstudianteYTutor(int tutorId, int studentId)
        {
            try
            {
                var citasCount = await _tutorServices.ContarCitasPorEstudianteYTutor(tutorId, studentId);
                return Ok(new { success = true, data = citasCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/obtenerInfoEstudiantePorTutor/{tutorId}/{studentId}")]
        public async Task<IActionResult> ObtenerInfoEstudiantePorTutor(int tutorId, int studentId)
        {
            try
            {
                var infoEstudiante = await _tutorServices.ObtenerInfoEstudiantePorTutor(tutorId, studentId);
                return Ok(new { success = true, data = infoEstudiante });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
