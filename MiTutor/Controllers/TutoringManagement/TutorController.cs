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

    }
}
