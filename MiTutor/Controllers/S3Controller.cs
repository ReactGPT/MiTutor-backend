using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace MiTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        private string bucketNameDef;

        public S3Controller(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            bucketNameDef = _configuration["S3:BucketName"];
            _s3Client = s3Client;
        }
        //Prueba1
        [HttpPost("uploadS3")]
        public async Task<IActionResult> UploadFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            try
            {
                // Nombre del bucket y clave del archivo
                string bucketName = bucketNameDef; // Reemplaza con el nombre de tu bucket
                string keyName = $"{folderName}/{file.FileName}";

                // Subir el archivo a S3
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = keyName,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.Private // Puedes cambiarlo a PublicRead si deseas que el archivo sea público
                    };

                    var fileTransferUtility = new TransferUtility(_s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                return Ok(new { Message = "File uploaded successfully", FileName = file.FileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Descargar archivo
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName, string carpeta)
        {
            var keyName = $"{carpeta}/{fileName}";

            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketNameDef,
                    Key = keyName
                };

                using (var response = await _s3Client.GetObjectAsync(request))
                using (var memoryStream = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    return File(memoryStream.ToArray(), "application/octet-stream", fileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Descargar archivo con nombre falso
        [HttpGet("downloadFalso/")]
        public async Task<IActionResult> DownloadFile(string fileNameReal, string fileNameFalso, string carpeta)
        {
            var keyName = $"{carpeta}/{fileNameReal}";

            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketNameDef,
                    Key = keyName
                };

                using (var response = await _s3Client.GetObjectAsync(request))
                using (var memoryStream = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    return File(memoryStream.ToArray(), "application/octet-stream", fileNameFalso);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Subir archivo
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var keyName = $"archivos/{file.FileName}";

            try
            {
                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);

                    var request = new PutObjectRequest
                    {
                        BucketName = bucketNameDef,
                        Key = keyName,
                        InputStream = fileStream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.Private
                    };

                    var response = await _s3Client.PutObjectAsync(request);
                    return Ok("Archivo cargado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Subir archivo con nombre y carpeta especificada
        [HttpPost("uploadAutomatic")]
        public async Task<IActionResult> UploadFile(IFormFile file, string fileName, string carpeta)
        {
            var keyName = $"{carpeta}/{fileName}";

            try
            {
                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);

                    var request = new PutObjectRequest
                    {
                        BucketName = bucketNameDef,
                        Key = keyName,
                        InputStream = fileStream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.Private
                    };

                    var response = await _s3Client.PutObjectAsync(request);
                    return Ok("Archivo cargado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
