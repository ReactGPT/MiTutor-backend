using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.UniversityUnitManagement;
using MiTutor.Services.UniversityUnitManagement;

namespace MiTutor.Controllers.UniversityUnitManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitDerivationController : ControllerBase
    {
        private readonly ILogger<UnitDerivationController> _logger;
        private readonly UnitDerivationService _unitDerivationService;

        public UnitDerivationController(ILogger<UnitDerivationController> logger, UnitDerivationService unitDerivationService)
        {
            _logger = logger;
            _unitDerivationService = unitDerivationService;
            _logger.LogInformation("Se inicio el constructor");
        }

        [HttpPost("/crearUnidadDerivacion")]
        public async Task<IActionResult> CrearUnidadDerivacion([FromBody] UnitDerivation unidad)
        {
            try
            {
                await _unitDerivationService.CrearUnidadDerivacion(unidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto la unidad satisfactoriamente" });
        }

        [HttpGet("/listarUnidades")]
        public async Task<IActionResult> ListarUnidadesDerivacion()
        {
            List<UnitDerivation> unidades;
            try
            {
                unidades = await _unitDerivationService.ListarUnidades();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = unidades });
        }

        [HttpGet("/listarSubUnidadesPorUnidad")]
        public async Task<IActionResult> ListarSubUnidadesPorUnidad(int unidadId)
        {
            List<UnitDerivation> unidades;
            try
            {
                unidades = await _unitDerivationService.ListarSubUnidadesPorUnidad(unidadId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = unidades });
        }

        [HttpPut("/actualizarUnidadDerivacion")]
        public async Task<IActionResult> ActualizarUnidadDerivacion([FromBody] UnitDerivation unidad)
        {
            try
            {
                await _unitDerivationService.ActualizarUnidadDerivacion(unidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizó la Unidad de Derivación satisfactoriamente" });
        }

        [HttpDelete("/eliminarUnidadDerivacion/{unidadId}")]
        public async Task<IActionResult> EliminarDerivacion(int unidadId)
        {
            try
            {
                await _unitDerivationService.EliminarUnidadDerivacion(unidadId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó la derivación satisfactoriamente" });
        }

        [HttpPost("/crearSubUnidadDerivacion")]
        public async Task<IActionResult> CrearSubUnidadDerivacion([FromBody] UnitDerivation unidad)
        {
            try
            {
                await _unitDerivationService.CrearSubUnidadDerivacion(unidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto la unidad satisfactoriamente" });
        }
    }
}
