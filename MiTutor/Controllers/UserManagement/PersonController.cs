using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services.UserManagement;

namespace MiTutor.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly PersonService _PersonServices;

        public PersonController(ILogger<PersonController> logger, PersonService PersonService)
        {
            _logger = logger;
            _PersonServices = PersonService;
        }

        [HttpPost("/crearPersona")]
        public async Task<IActionResult> CrearPersona([FromBody] Person Person)
        {
            try
            {
                await _PersonServices.CrearPersona(Person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se inserto satisfactoriamente" });
        }
    }
}
