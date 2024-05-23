using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorStudentProgramController : ControllerBase
    {
        private readonly ILogger<TutorStudentProgramController> _logger;
        private readonly TutorStudentProgramService _tutorServices;

        public TutorStudentProgramController(ILogger<TutorStudentProgramController> logger, TutorStudentProgramService tutorService)
        {
            _logger = logger;
            _tutorServices = tutorService;
        }
        [HttpPost("/crearTutorStudentProgram")]
        public async Task<IActionResult> CrearTutorStudentProgram([FromBody] TutorStudentProgramModificado tutorStudentProgram)
        {
            try
            {
                await _tutorServices.CrearTutorStudentProgram(tutorStudentProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertó satisfactoriamente" });
        }
    }
}
