using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly AppointmentService _appointmentServices;

        public AppointmentController(ILogger<AppointmentController> logger, AppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentServices = appointmentService;
        }

        [HttpPost("/agregarCita")]
        public async Task<IActionResult> AgregarCita([FromBody] RegisterAppointment registerAppointment)
        {
            try
            {
                await _appointmentServices.AgregarCita(registerAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("/listarCitasPorTutor/{tutorId}")]
        public async Task<IActionResult> ListarCitasPorTutor(int tutorId)
        {
            try
            {

                var citas = await _appointmentServices.ListarCitasPorTutor(tutorId);


                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarCitasPorTutorPorAlumno/{tutorId}/{studentId}")]
        public async Task<IActionResult> ListarCitasPorTutorPorAlumno(int tutorId, int studentId)
        {
            try
            {

                var citas = await _appointmentServices.ListarCitasPorTutorPorAlumno(tutorId, studentId);


                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarCitasPorAlumno/{studentId}")]
        public async Task<IActionResult> ListarCitasPorAlumno(int studentId)
        {
            try
            {

                var citas = await _appointmentServices.ListarCitasPorAlumno(studentId);


                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarCitasPorID/{appointId}")]
        public async Task<IActionResult> ListarCitasPorID(int appointId)
        {
            try
            {

                var citas = await _appointmentServices.ListarCitasPorID(appointId);


                return Ok(new { success = true, data = citas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
