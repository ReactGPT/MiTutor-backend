using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly FilesService _filesServices;

        public FilesController(ILogger<FilesController> logger, FilesService filesService)
        {
            _logger = logger;
            _filesServices = filesService;
        }

        [HttpPost("/insertarArchivoBD")]
        public async Task<IActionResult> InsertarArchivo([FromBody] Files file)
        {
            try
            {
                int id = await _filesServices.InsertarArchivo(file);
                return Ok(new { success = true, message = "Se insertó satisfactoriamente", idArchivo = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al insertar el archivo", error = ex.Message });
            }
        }

        [HttpPost("/insertarArchivoAlumnoBD")]
        public async Task<IActionResult> InsertarArchivoAlumno([FromBody] Files file)
        {
            try
            {
                int id = await _filesServices.InsertarArchivoAlumno(file);
                return Ok(new { success = true, message = "Se insertó satisfactoriamente", idArchivo = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error al insertar el archivo", error = ex.Message });
            }
        }

        [HttpGet("/listarArchivos/{idResultado}/{idTipo}")]
        public async Task<IActionResult> ListarArchivosPorIdResultadoTipo(int idResultado, int idTipo)
        {
            try
            {
                List<FileBD> archivos = await _filesServices.ListarArchivosPorIdResultadoTipo(idResultado, idTipo);
                return Ok(new { success = true, data = archivos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/listarArchivosAlumno/{idAlumno}")]
        public async Task<IActionResult> ListarArchivosPorIdAlumno(int idAlumno)
        {
            try
            {
                List<FileBD> archivos = await _filesServices.ListarArchivosPorIdAlumno(idAlumno);
                return Ok(new { success = true, data = archivos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("/eliminarArchivo")]
        public async Task<IActionResult> EliminarArchivo([FromQuery] int fileId)
        {
            try
            {
                await _filesServices.EliminarArchivo(fileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se eliminó satisfactoriamente." });
        }

        [HttpPut("/reactivarArchivo")]
        public async Task<IActionResult> ReactivarArchivo([FromQuery] int fileId)
        {
            try
            {
                await _filesServices.ReactivarArchivo(fileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { success = true, message = "Se ACTIVO satisfactoriamente." });
        }
    }
}
