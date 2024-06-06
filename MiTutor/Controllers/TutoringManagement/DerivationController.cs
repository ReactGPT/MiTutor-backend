using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;
using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerivationController : ControllerBase
    {
        private readonly ILogger<DerivationController> _logger;
        private readonly DerivationService _derivationServices;

        public DerivationController(ILogger<DerivationController> logger, DerivationService derivationServices)
        {
            _logger = logger;
            _derivationServices = derivationServices;
        }

        [HttpPost("/crearDerivacion")]
        public async Task<IActionResult> CrearDerivacion([FromBody] Derivation derivation)
        {
            try
            {
                int derivationId = await _derivationServices.CrearDerivacion(derivation);
                // Devolver el ID de la derivación creada en la respuesta
                return Ok(new { success = true, message = "Se insertaron satisfactoriamente", data = derivationId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarDerivaciones")]
        public async Task<IActionResult> ListarDerivaciones()
        {
            List<Derivation> derivations;
            try
            {
                derivations = await _derivationServices.ListarDerivations();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = derivations });
        }

        [HttpPut("/actualizarDerivacion")]
        public async Task<IActionResult> ActualizarDerivacion([FromBody] Derivation derivation)
        {
            try
            {
                await _derivationServices.ActualizarDerivacion(derivation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizó la derivación satisfactoriamente" });
        }

        [HttpDelete("/eliminarDerivacion/{derivationId}")]
        public async Task<IActionResult> EliminarDerivacion(int derivationId)
        {
            try
            {
                await _derivationServices.EliminarDerivacion(derivationId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó la derivación satisfactoriamente" });
        }

        [HttpGet("/listarUnidadesDerivacion")]
        public async Task<IActionResult> ListarUnidadesDerivacion()
        {
            List<UnitDerivation> derivations;
            try
            {
                derivations = await _derivationServices.ListarUnidadesDerivacion();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = derivations });
        }

        [HttpGet("/seleccionarDerivacionByIdAppointment/{appointmentId}")]
        public async Task<IActionResult> SeleccionarDerivacionByIdAppointment(int appointmentId)
        {
            Derivation derivation;
            try
            {
                derivation = await _derivationServices.SeleccionarDerivacionByIdAppointment(appointmentId);
                return Ok(new { success = true, data = derivation });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
