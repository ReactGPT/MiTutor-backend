using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityTutorController : ControllerBase
    {
        private readonly ILogger<AvailabilityTutorController> _logger;
        private readonly AvailabilityTutorService _AvailabilityTutorService;

        public AvailabilityTutorController(ILogger<AvailabilityTutorController> logger, AvailabilityTutorService AvailabilityTutorService)
        {
            _logger = logger;
            _AvailabilityTutorService = AvailabilityTutorService;
        }

        [HttpGet("/listarDisponibilidadPorTutor/{tutorId}")]
        public async Task<IActionResult> ListarDisponibilidadPorTutor(int tutorId)
        {
            try
            {
                var programas = await _AvailabilityTutorService.ListarDisponibilidadPorTutor(tutorId);
                return Ok(new { success = true, data = programas });
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/insertarDisponibilidad")]
        public async Task<IActionResult> InsertarDisponibilidad([FromBody] CreateAvailabilityTutor createAvailability)
        {
            if (createAvailability == null)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                await _AvailabilityTutorService.InsertarDisponibilidad(createAvailability);
                return Ok(new { AvailabilityTutorId = createAvailability.AvailabilityTutorId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("/eliminarDisponibilidad/{availabilityTutorId}")]
        public async Task<IActionResult> EliminarDisponibilidad(int availabilityTutorId)
        {
            try
            {
                bool eliminado = await _AvailabilityTutorService.EliminarDisponibilidad(availabilityTutorId);
                if (eliminado)
                {
                    return Ok(new { success = true, message = "Se elimino correctamente." });
                }
                else
                {
                    return NotFound(new { success = false, message = "No se encontro la disponibilidad." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
