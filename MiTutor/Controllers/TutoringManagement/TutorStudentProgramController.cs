using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

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

        [HttpGet("listarSolicitudesPorFacultad/{facultyId}")]
        public async Task<IActionResult> ListarSolicitudesPorFacultad(int facultyId)
        {
            List<Solicitud> solicitudes;
            try
            {
                solicitudes = await _tutorServices.ListarSolicitudesPorFacultad(facultyId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = solicitudes });
        }

        [HttpGet("listarTutorStudentProgram")]
        public async Task<IActionResult> ListarTutorStudentProgram([FromQuery] string tutorFirstName = null, [FromQuery] string tutorLastName = null, [FromQuery] string state = null, [FromQuery] int? tutoringProgramId = null)
        {
            List<TutorStudentProgram> tutorStudentPrograms;
            try
            {
                tutorStudentPrograms = await _tutorServices.ListarTutorStudentProgram(tutorFirstName, tutorLastName, state, tutoringProgramId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los TutorStudentPrograms: " + ex.Message });
            }
            return Ok(new { success = true, data = tutorStudentPrograms });
        }

        [HttpPost("UpdateEstado")]
        public async Task<IActionResult> UpdateEstado([FromBody] UpdateEstadoRequest request)
        {
            try
            {
                await _tutorServices.ActualizarEstadoTutorStudentProgram(request.TutorStudentProgramIds, request.NewState);
                return Ok(new { success = true, message = "Estado actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }

    public class UpdateEstadoRequest
    {
        public string TutorStudentProgramIds { get; set; }
        public string NewState { get; set; }
    }
}
