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
    }
}
