using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace MiTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;

        public S3Controller(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            _s3Client = s3Client;
        }

        [HttpPost("uploadS3")]
        public async Task<IActionResult> UploadFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            try
            {
                // Nombre del bucket y clave del archivo
                string bucketName = "bucket11-2"; // Reemplaza con el nombre de tu bucket
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
    }
}
