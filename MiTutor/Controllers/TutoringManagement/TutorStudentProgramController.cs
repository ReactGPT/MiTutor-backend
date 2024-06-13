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
        private readonly TutorStudentProgramService _tutorStudentProgramService; // Corregido el nombre del servicio
        private readonly TutorService _tutorService;
        private readonly StudentProgramService _studentProgramService;

        public TutorStudentProgramController(ILogger<TutorStudentProgramController> logger, TutorStudentProgramService tutorStudentProgramService) // Corregido el nombre del parámetro del constructor
        {
            _logger = logger;
            _tutorStudentProgramService = tutorStudentProgramService; // Corregido el nombre de la variable del servicio
        }

        [HttpPost("/crearTutorStudentProgram")]
        public async Task<IActionResult> CrearTutorStudentProgram([FromBody] TutorStudentProgramModificado tutorStudentProgram)
        {
            try
            {
                await _tutorStudentProgramService.CrearTutorStudentProgram(tutorStudentProgram);
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
                solicitudes = await _tutorStudentProgramService.ListarSolicitudesPorFacultad(facultyId);
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
            try
            {
                var tutorStudentPrograms = await _tutorStudentProgramService.ListarTutorStudentProgram(tutorFirstName, tutorLastName, state, tutoringProgramId);
                return Ok(new { success = true, data = tutorStudentPrograms });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al listar los TutorStudentPrograms: " + ex.Message });
            }
        }

        [HttpPost("UpdateEstado")]
        public async Task<IActionResult> UpdateEstado([FromBody] UpdateEstadoRequest request)
        {
            try
            {
                await _tutorStudentProgramService.ActualizarEstadoTutorStudentProgram(request.TutorStudentProgramIds, request.NewState);
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
