using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.UniversityUnitManagement;
using MiTutor.Services.UniversityUnitManagement;

namespace MiTutor.Controllers.UniversityUnitManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ILogger<SpecialtyController> _logger;
        private readonly SpecialtyService _specialtyServices;

        public SpecialtyController(ILogger<SpecialtyController> logger, SpecialtyService specialtyService)
        {
            _logger = logger;
            _specialtyServices = specialtyService;
        }

        [HttpPost("crearEspecialidad")] // Cambiado a ruta relativa
        public async Task<IActionResult> CrearEspecialidad([FromBody] Specialty specialty)
        {
            try
            {
                await _specialtyServices.CrearEspecialidad(specialty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }

        [HttpPost("modificarEspecialidad")] // Cambiado a ruta relativa
        public async Task<IActionResult> ModificarEspecialidad([FromBody] Specialty specialty)
        {
            try
            {
                await _specialtyServices.ModificarEspecialidad(specialty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se modificó satisfactoriamente" });
        }


        [HttpGet("listarEspecialidad")] // Cambiado a ruta relativa
        public async Task<IActionResult> ListarEspecialidades()
        {
            List<Specialty> specialties;
            try
            {
                specialties = await _specialtyServices.ListarEspecialidades();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = specialties });
        }

<<<<<<< HEAD
        [HttpGet("listarEspecialidadXNombre")] // Cambiado a ruta relativa
        public async Task<IActionResult> ListarEspecialidadesXNombre([FromQuery(Name = "name")] string name)
=======
        [HttpGet("listarEspecialidadPorFacultad")] // Cambiado a ruta relativa
        public async Task<IActionResult> ListarEspecialidadesPorFacultad(int FacultyId)
>>>>>>> 7b548bdb7f50c21eca390c6269087d8a2dee3d38
        {
            List<Specialty> specialties;
            try
            {
<<<<<<< HEAD
                specialties = await _specialtyServices.ListarEspecialidadesXNombre(name==null?"":name);
=======
                specialties = await _specialtyServices.ListarEspecialidadesPorFacultad(FacultyId);
>>>>>>> 7b548bdb7f50c21eca390c6269087d8a2dee3d38
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = specialties });
        }
    }
}
