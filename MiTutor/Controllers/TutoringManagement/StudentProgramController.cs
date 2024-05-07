using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProgramController : ControllerBase
    {
        private readonly ILogger<StudentProgramController> _logger;
        private readonly StudentProgramService _studentProgramService;

        public StudentProgramController(ILogger<StudentProgramController> logger, StudentProgramService studentProgramService)
        {
            _logger = logger;
            _studentProgramService = studentProgramService;
        }

        [HttpPost("/crearProgramaEstudiante")]
        public async Task<IActionResult> CrearTutor([FromBody] StudentProgram studentProgram)
        {
            try
            {
                await _studentProgramService.CrearProgramaEstudiante(studentProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

    }
}
