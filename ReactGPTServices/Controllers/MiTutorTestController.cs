using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//USAR ESTE CONTROLLER COMO PLANTILLA
using DBMiTutor;
using System.Data;
using TutorPUCPServices.Models;
namespace ReactGPTServices.Controllers
{
    [ApiController]
    [Route("/especialidad")]
    public class MiTutorTestController : ControllerBase
    {
        
        private readonly ILogger<MiTutorTestController> _logger;

        public MiTutorTestController(ILogger<MiTutorTestController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "[NOMBRE DE API , SINO UTILIZARA EL NOMBRE DEL CONTROLLER]")]
        [HttpPost("/crearEspecialidad")] //El api aqui usa el nombre del controller
        async public Task<IActionResult> CrearEspecialidad([FromBody] CrearEspecialidadRequest especialidadRequest)
        {
            try {
                
                //Alguna Logica
                //Para llamar un SP
                MiTutorDB basededatos = new();
                //string respuestaEnJson = JsonConvert.SerializeObject(basededatos.ValidarCuentaUsuario("test","test@test"));// Convierte lo que sea que retorne la DB en un string JSON
                //DataTable dt = basededatos.CatalogoFiltros(1,"PERU","C#",2024);//Trabaja el resultado que trae la BD como DataTable.
                int rows = basededatos.ESP_InsertarEspecilidad(especialidadRequest.nombre, especialidadRequest.username);


            }
            catch(Exception ex) {
                //Si hay error, el servicio devolvera los parametros necesarios
                return BadRequest(new
                {
                    mensaje = ex.ToString(),
                    success = false
                }) ;
            }
            return Ok(new { success=true , message="Se intertaron satisfactoriamente"});
        }

        [HttpGet("/listarEspecialidades")] //El api aqui usa el nombre del controller
        async public Task<IActionResult> ListarEspecialidades()
        {
            try
            {

                //Alguna Logica
                //Para llamar un SP
                MiTutorDB basededatos = new();
                
                //string respuestaEnJson = JsonConvert.SerializeObject(basededatos.ValidarCuentaUsuario("test","test@test"));// Convierte lo que sea que retorne la DB en un string JSON
                //DataTable dt = basededatos.CatalogoFiltros(1,"PERU","C#",2024);//Trabaja el resultado que trae la BD como DataTable.
                DataTable dt = basededatos.ESP_ListarEspecialidades();
                return Ok(new { success = true, data=JsonConvert.SerializeObject(dt) ,message = "" });

            }
            catch (Exception ex)
            {
                //Si hay error, el servicio devolvera los parametros necesarios
                return BadRequest(new
                {
                    mensaje = ex.ToString(),
                    success = false
                });
            }
            
        }

        [HttpDelete("/eliminarEspecialidad/{id}")]
        public async Task<IActionResult> EliminarEspecialidad(int id)
        {
            try
            {
                MiTutorDB basededatos = new();
                int rows = basededatos.ESP_EliminarEspecialidad(id);
                if (rows > 0)
                    return Ok(new { success = true, message = "Se eliminó la especialidad correctamente" });
                else
                    return NotFound(new { success = false, message = "La especialidad no fue encontrada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.ToString(), success = false });
            }
        }

        [HttpPut("/actualizarEspecialidad/{id}")]
        public async Task<IActionResult> ActualizarEspecialidad(int id, string nombre)
        {
            try
            {
                MiTutorDB basededatos = new();
                int rows = basededatos.ESP_ActualizarEspecialidad(id, nombre);
                if (rows > 0)
                    return Ok(new { success = true, message = "Se actualizó la especialidad correctamente" });
                else
                    return NotFound(new { success = false, message = "La especialidad no fue encontrada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.ToString(), success = false });
            }
        }
    }
}
