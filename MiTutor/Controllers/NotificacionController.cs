using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services;

namespace MiTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {

        private readonly ILogger<NotificacionController> _logger;
        private readonly NotificacionService _notificacionService;

        public NotificacionController(ILogger<NotificacionController> logger, NotificacionService notificacionService)
        {
            _logger = logger;
            _notificacionService = notificacionService;
        }

        [HttpGet("/listarNotificaciones")]
        public async Task<IActionResult> ListarNotificaciones([FromQuery] int userAcountId) 
        {
            List<Notificacion> notificaciones;

            try
            {
                notificaciones = await _notificacionService.ListarNotificacionesPorUserAcount(userAcountId);
                return Ok(new { success = true, data = notificaciones });
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en controller", ex);
            }

        }

    }
}
