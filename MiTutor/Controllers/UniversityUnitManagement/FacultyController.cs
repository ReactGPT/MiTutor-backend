using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.UniversityUnitManagement;
using MiTutor.Services;
using MiTutor.Services.UniversityUnitManagement;

namespace MiTutor.Controllers.UniversityUnitManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly ILogger<FacultyController> _logger;
        private readonly FacultyService _facultyServices;

        public FacultyController(ILogger<FacultyController> logger, FacultyService facultyService)
        {
            _logger = logger;
            _facultyServices = facultyService;
        }

        [HttpPost("crearFacultad")]  // Cambiado a ruta relativa
        public async Task<IActionResult> CrearFacultad([FromBody] Faculty faculty)
        {
            try
            {
                await _facultyServices.CrearFacultad(faculty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertó satisfactoriamente" });
        }

        [HttpGet("listarFacultades")]  // Cambiado a ruta relativa
        public async Task<IActionResult> ListarFacultades()
        {
            List<Faculty> faculties;
            try
            {
                faculties = await _facultyServices.ListarFacultades();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = faculties });
        }

        [HttpGet("listarFacultadesTodos")]  // Cambiado a ruta relativa
        public async Task<IActionResult> ListarFacultadesTodos()
        {
            List<Faculty> faculties;
            try
            {
                faculties = await _facultyServices.ListarFacultadesTodos();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = faculties });
        }

        [HttpPut("actualizarFacultades")]  // Cambiado a ruta relativa
        public async Task<IActionResult> ActualizarFacultad([FromBody] Faculty facultad)
        {
            try
            {
                await _facultyServices.ActualizarFacultad(facultad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente datos de facultad." });
        }

        [HttpDelete("eliminarFacultad/{facultadId}")]
        public async Task<IActionResult> EliminarFacultad(int facultadId)
        {
            try
            {
                await _facultyServices.EliminarFacultad(facultadId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó satisfactoriamente." });
        }
    }
}
