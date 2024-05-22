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
        public async Task<IActionResult> AgregarResultadoCita([FromBody] InsertAppointmentResult appointmentResult)
        {
            try
            {
                await _appointmentResultServices.AgregarResultadoCita(appointmentResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
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
    }
}
