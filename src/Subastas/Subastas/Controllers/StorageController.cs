using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Subastas.Dto.S3;
using Subastas.Interfaces.Services;
using S3Object = Subastas.Dto.S3.S3Object;

namespace Subastas.Controllers
{
    public class StorageController : Controller
    {
        private readonly IS3StorageService _is3StorageService;
        private readonly IConfiguration _config;
        private readonly ILogger<StorageController> _logger;
        private readonly string _bucketName;
        private readonly AwsCredentials _awsCredentials;
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;

        public StorageController(IS3StorageService is3StorageService, IConfiguration config, ILogger<StorageController> logger)
        {
            _is3StorageService = is3StorageService;
            _config = config;
            _logger = logger;
            _bucketName = _config["AwsConfiguration:SubastasBucket"];
            _awsCredentials = new AwsCredentials
            {
                AwsKey = _config["AwsConfiguration:AwsAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AwsSecretKey"]
            };
            _contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (!IsFileValid(file, out var validationMessage))
            {
                return Content(validationMessage);
            }

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileExt = Path.GetExtension(file.FileName);
            var objName = $"{Guid.NewGuid()}{fileExt}";

            var s3Object = new S3Object
            {
                BucketName = _bucketName,
                InputStream = memoryStream,
                Name = objName
            };

            var response = await _is3StorageService.UploadObjectAsync(s3Object, _awsCredentials);
            return Content(response.Message);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFile(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                return Content("file not specified");
            }

            var s3File = new S3File
            {
                BucketName = _bucketName,
                KeyName = keyName
            };

            var response = await _is3StorageService.DeleteObjectAsync(_awsCredentials, s3File);
            return Content(response.Message);
        }
        
        [HttpGet]
        public IActionResult ShowImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowImage(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                ViewBag.ErrorMessage = "Please provide an image key.";
                return View();
            }

            try
            {
                var awsCredentials = new AwsCredentials
                {
                    AwsKey = _awsCredentials.AwsKey,
                    AwsSecretKey = _awsCredentials.AwsSecretKey
                };

                var fileContent = await _is3StorageService.GetObjectAsync(awsCredentials, new S3File
                {
                    BucketName = _bucketName,
                    KeyName = keyName
                });

                ViewBag.ImageData = Convert.ToBase64String(fileContent.Data);
                ViewBag.ContentType = fileContent.ContentType;
                ViewBag.KeyName = keyName;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View();
        }

        private bool IsFileValid(IFormFile file, out string validationMessage)
        {
            if (file == null || file.Length == 0)
            {
                validationMessage = "file not selected";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }
    }
}
