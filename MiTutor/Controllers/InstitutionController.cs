using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services;

namespace MiTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly ILogger<InstitutionController> _logger;
        private readonly InstitutionService _institutionServices;

        public InstitutionController(ILogger<InstitutionController> logger, InstitutionService institutionServices)
        {
            _logger = logger;
            _institutionServices = institutionServices;

            _logger.LogInformation("Se inicio el constructor");
        }

        [HttpPost("/crearInstitucion")]
        public async Task<IActionResult> CrearInstitucion([FromBody] Institution institucion)
        {
            try
            {
                await _institutionServices.CrearInstitucion(institucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }

        [HttpGet("/listarInstitucion")]
        public async Task<IActionResult> ListarInstitucion()
        {
            List<Institution> instituciones;
            try
            {
                instituciones = await _institutionServices.ListarInstituciones();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = instituciones });
        }

        [HttpPost("/actualizarInstitucion")]
        public async Task<IActionResult> ActualizarInstitucion([FromBody] Institution institucion)
        {
            try
            {
                await _institutionServices.ActualizarInstitucion(institucion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizó la Institucion satisfactoriamente" });
        }
    }
}
