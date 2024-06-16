using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentResultController : ControllerBase
    {
        private readonly ILogger<AppointmentResultController> _logger;
        private readonly AppointmentResultService _appointmentResultServices;

        public AppointmentResultController(ILogger<AppointmentResultController> logger, AppointmentResultService appointmentResultService)
        {
            _logger = logger;
            _appointmentResultServices = appointmentResultService;
        }

        [HttpPost("/agregarResultadoCita")]
        public async Task<IActionResult> AgregarResultadoCita([FromQuery] int studentId, [FromQuery] int tutoringProgramId, [FromQuery] int id_appointment)
        {
            try
            {
                await _appointmentResultServices.AgregarResultadoCita(studentId,tutoringProgramId, id_appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpPost("/agregarResultadoCitaOriginal")]
        public async Task<IActionResult> AgregarResultadoCitaOriginal([FromBody] InsertAppointmentResult appointmentResult, DateTime startTime, DateTime endTime) { 
            try
            { 
                List<int> ids = await _appointmentResultServices.AgregarResultadoCitaOriginal(appointmentResult,startTime,endTime);
                // Devolver el ID de la derivación creada en la respuesta
                return Ok(new { success = true, message = "Se insertaron satisfactoriamente", data = ids });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpGet("/consultarResultadoCita")]
        public async Task<IActionResult> ConsultarResultadoCita([FromQuery] int appointmentId, [FromQuery] int studentId, [FromQuery] int tutoringProgramId)
        {
            InsertAppointmentResult appointmentResult;
            try
            {
                appointmentResult = await _appointmentResultServices.ConsultarResultadoCita(appointmentId, studentId, tutoringProgramId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok( appointmentResult );
        }
        /*APPOINTMENTRESULT_ACTUALIZAR_UPDATE*/
        [HttpPut("/actualizarResultadoCita")]
        public async Task<IActionResult> ActualizarResultadoCita([FromQuery] int id_appointmentResult, [FromQuery] bool asistio, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        //ActualizarResultadoCita(int id_appointmentResult, bool asistio, DateTime startTime, DateTime endTime)
        {
            try
            {
                await _appointmentResultServices.ActualizarResultadoCita(id_appointmentResult,asistio, startTime, endTime);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente" });
        }

        [HttpPost("/guardarAsistenciasCitaGrupal")]
        public async Task<ActionResult<List<int>>> GuardarAsistencias([FromBody] List<ListarStudentJSON2> estudiantes)
        {
            if (estudiantes == null || estudiantes.Count == 0)
            {
                return BadRequest("No se proporcionaron datos de estudiantes.");
            }

            try
            {
                var idsInsertados = await _appointmentResultServices.AgregarResultadosCitaGrupal(estudiantes);
                return Ok(idsInsertados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar asistencias: {ex.Message}");
            }
        }

        [HttpPut("/actualizarResultadoCitaGrupal")]
        public async Task<IActionResult> ActualizarResultadoCitaGrupal([FromBody] List<ListarStudentJSON2> estudiantes)
        {
            if (estudiantes == null || estudiantes.Count == 0)
            {
                return BadRequest("La lista de estudiantes está vacía o no se ha recibido correctamente.");
            }

            try
            {
                await _appointmentResultServices.ActualizarResultadosCitaGrupal(estudiantes);
                return Ok(new { success = true, message = "Se actualizaron satisfactoriamente" });
            }
            catch (Exception ex)
            {
                // Log para depuración
                Console.WriteLine($"Error al actualizar resultados: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
