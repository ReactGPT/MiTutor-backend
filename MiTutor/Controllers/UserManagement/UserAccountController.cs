using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.UserManagement;

namespace MiTutor.Controllers.GestionUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly ILogger<UserAccountController> _logger;
        private readonly UserAccountService _usuarioServices;

        public UserAccountController(ILogger<UserAccountController> logger, UserAccountService userAccountService)
        {
            _logger = logger;
            _usuarioServices = userAccountService;

            _logger.LogInformation("Se inicio el constructor");
        }

        [HttpPost("/crearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UserAccount userAccount)
        {
            try
            {
                await _usuarioServices.CrearUsuario(userAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }

        [HttpGet("/listarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            List<UserAccount> userAccounts;
            try
            {
                userAccounts = await _usuarioServices.ListarUsuarios();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = userAccounts });
        }


    }
}
