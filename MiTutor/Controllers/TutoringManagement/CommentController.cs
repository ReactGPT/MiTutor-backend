using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly CommentService _commentServices;

        public CommentController(ILogger<CommentController> logger, CommentService commentServices)
        {
            _logger = logger;
            _commentServices = commentServices;
        }

        [HttpPost("/crearComentario")]
        public async Task<IActionResult> CrearComentario([FromBody] Comment comment)
        {
            try
            {
                await _commentServices.CrearComentario(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se insertaron satisfactoriamente" });
        }

        [HttpGet("/listarComentarios")]
        public async Task<IActionResult> ListarComentarios()
        {
            List<Comment> comments;
            try
            {
                comments = await _commentServices.ListarComments();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, data = comments });
        }

        [HttpPut("/actualizarComentario")]
        public async Task<IActionResult> ActualizarComentario([FromBody] Comment comment)
        {
            try
            {
                await _commentServices.ActualizarComentario(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se actualizaron satisfactoriamente" });
        }

        [HttpDelete("/eliminarComentario/{commentId}")]
        public async Task<IActionResult> EliminarComentario(int commentId)
        {
            try
            {
                await _commentServices.EliminarComentario(commentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó el comentario satisfactoriamente" });
        }

    }
}
