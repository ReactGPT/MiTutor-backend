using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Services;

namespace MiTutor.Controllers
{
    [Route("/especialidad")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        /*
        private readonly ILogger<EspecialidadController> _logger;
        private readonly EspecialidadService _especialidadServices;

        public EspecialidadController(ILogger<EspecialidadController> logger, EspecialidadService especialidadService)
        {
            _logger = logger;
            _especialidadServices = especialidadService;
        }

        [HttpPost("/crearEspecialidad")]
        public async Task<IActionResult> CrearEspecialidad([FromBody] Especialidad especialidad)
        {
            try
            {
                await _especialidadServices.CrearEspecialidad(especialidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }

        [HttpGet("/listarEspecialidades")]
        public async Task<IActionResult> ListarEspecialidades()
        {
            List<Especialidad> especialidades;
            try
            {
                especialidades = await _especialidadServices.ListarEspecialidades();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = especialidades });
        }

        [HttpPut("/actualizarEspecialidad")]
        public async Task<IActionResult> ActualizarEspecialidad([FromBody] Especialidad especialidad)
        {
            try
            {
                await _especialidadServices.ActualizarEspecialidad(especialidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente" });
        }
        */
    }
}
