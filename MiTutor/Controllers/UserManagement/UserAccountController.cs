using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.GestionUsuarios;
using MiTutor.Services;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.UserManagement;
using System.Data;
using System.Text.Json;

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

        [HttpPost("/eliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioServices.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó correctamente el usuario" });
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

        [HttpGet("/userInfo")]
        public async Task<IActionResult> ObtenerInfoUsuario(string email=null,string codigoPUCP=null)
        {

            UserAccount user=new UserAccount
            {
                isVerified = false
            };
            string json = "";
            try
            {
                //var options = new JsonSerializerOptions
                //{
                //    Converters = { new UserGenericConverter() },
                //    WriteIndented = true
                //};
                user = await _usuarioServices.ObtenerInfoUsuario(email,codigoPUCP);
                //json = JsonSerializer.Serialize(user.Roles, options);
                //var deserializedRoles = JsonSerializer.Deserialize<List<UserGeneric>>(json, options);
                //user.Roles = deserializedRoles;
            }
            catch (Exception ex)
            {
                return BadRequest(new {success=false,message= ex.Message,data=user});
            }
            return Ok(new { success = true, data = user });
        }


    }
}
