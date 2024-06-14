using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services;
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

        [HttpGet("/listarNotificaciones/{userAcountId}")]
        public async Task<IActionResult> ListarNotificaciones(int userAcountId)
        {
            List<Notificacion> notificaciones;

            try
            {
                notificaciones = await _studentProgramService.ListarNotificacionesPorUserAcount(userAcountId);
                return Ok(new { success = true, data = notificaciones });
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en controller", ex);
            }

        }

    }

}
