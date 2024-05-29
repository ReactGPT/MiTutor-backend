using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.TutoringManagement;
using Renci.SshNet;
using System.IO;

namespace MiTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ArchivosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName, string carpeta)
        {
            var sftpHost = "3.218.128.38";  
            var sftpUsername = "ubuntu";
            //var sftpPrivateKeyPath = "C:\\Users\\admin\\Desktop\\22-05-2024-miTutor\\MiTutor-backend\\MiTutor\\Controllers\\nuevo.pem";
            var projectDirectory = Directory.GetCurrentDirectory();
            var sftpPrivateKeyRelativePath = Path.Combine("Controllers", "nuevo.pem");
            var sftpPrivateKeyPath = Path.Combine(projectDirectory, sftpPrivateKeyRelativePath);

            var sftpRemotePath = $"/home/ubuntu/{carpeta}/{fileName}";   

            using (var sftp = new SftpClient(sftpHost, sftpUsername, new PrivateKeyFile(sftpPrivateKeyPath)))
            {
                sftp.Connect();
                if (sftp.IsConnected)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        sftp.DownloadFile(sftpRemotePath, memoryStream);
                        sftp.Disconnect();

                        return File(memoryStream.ToArray(), "application/octet-stream", fileName);
                    }
                }
                else
                {
                    Console.WriteLine("no se logro conectar");
                    return null; 
                }
            }
        
        }

        [HttpGet("downloadFalso/")]
        public IActionResult DownloadFile(string fileNameReal, string fileNameFalso, string carpeta)
        {
            var sftpHost = "3.218.128.38";
            var sftpUsername = "ubuntu";
            //var sftpPrivateKeyPath = "C:\\Users\\admin\\Desktop\\22-05-2024-miTutor\\MiTutor-backend\\MiTutor\\Controllers\\nuevo.pem";
            var projectDirectory = Directory.GetCurrentDirectory();
            var sftpPrivateKeyRelativePath = Path.Combine("Controllers", "nuevo.pem");
            var sftpPrivateKeyPath = Path.Combine(projectDirectory, sftpPrivateKeyRelativePath);

            var sftpRemotePath = $"/home/ubuntu/{carpeta}/{fileNameReal}";

            using (var sftp = new SftpClient(sftpHost, sftpUsername, new PrivateKeyFile(sftpPrivateKeyPath)))
            {


                sftp.Connect();
                if (sftp.IsConnected)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        sftp.DownloadFile(sftpRemotePath, memoryStream);
                        sftp.Disconnect();

                        return File(memoryStream.ToArray(), "application/octet-stream", fileNameFalso);
                    }
                }
                else
                {
                    Console.WriteLine("no se logro conectar");
                    return null;
                }
            }

        }

        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile file)
        {
            var sftpHost = "3.218.128.38";
            var sftpUsername = "ubuntu";
            var sftpPrivateKeyPath = "C:\\Users\\admin\\Desktop\\22-05-2024-miTutor\\MiTutor-backend\\MiTutor\\Controllers\\nuevo.pem";
            var sftpRemotePath = "/home/ubuntu/archivos/" + file.FileName;

            using (var sftp = new SftpClient(sftpHost, sftpUsername, new PrivateKeyFile(sftpPrivateKeyPath)))
            {
                sftp.Connect();
                if (sftp.IsConnected)
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        sftp.UploadFile(fileStream, sftpRemotePath);
                        sftp.Disconnect();

                        return Ok("Archivo cargado exitosamente.");
                    }
                }
                else
                {
                    Console.WriteLine("No se logró conectar");
                    return StatusCode(StatusCodes.Status500InternalServerError, "No se logró conectar al servidor SFTP.");
                }
            }
        }

        //ENVIO AUTOMATICO
        //IFormFile file, string fileName
        [HttpPost("uploadAutomatic")]
        public IActionResult UploadFile(IFormFile file, string fileName, string carpeta)
        {
            var sftpHost = "3.218.128.38";
            var sftpUsername = "ubuntu";
            var sftpPrivateKeyPath = "C:\\Users\\admin\\Desktop\\22-05-2024-miTutor\\MiTutor-backend\\MiTutor\\Controllers\\nuevo.pem";
            //var sftpRemotePath = "/home/ubuntu/archivos/" + file.FileName;
            var sftpRemotePath = $"/home/ubuntu/{carpeta}/{fileName}";
            using (var sftp = new SftpClient(sftpHost, sftpUsername, new PrivateKeyFile(sftpPrivateKeyPath)))
            {
                sftp.Connect();
                if (sftp.IsConnected)
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        sftp.UploadFile(fileStream, sftpRemotePath);
                        sftp.Disconnect();

                        return Ok("Archivo cargado exitosamente.");
                    }
                }
                else
                {
                    Console.WriteLine("No se logró conectar");
                    return StatusCode(StatusCodes.Status500InternalServerError, "No se logró conectar al servidor SFTP.");
                }
            }
        }


    }
}
