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

        [HttpPost("/crearFacultad")]
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
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpGet("/listarFacultades")]
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
    }
}
