using Common;
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace RoadsidebiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IWebHostEnvironment _appEnvironment;
        private readonly ILogger<FoodController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        public FoodController(IWebHostEnvironment _environment, ILogger<FoodController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _appEnvironment = _environment;
            _logger = logger;
            _httpContext = httpContextAccessor;
        }
        #region Food Catagory 
        [HttpGet]
        [Route("Getallfoodcaragories")]
        public OperationResponse Getallfoodcaragories()
        {
            _logger.LogInformation("Started Getallfoodcaragories at {Time}. Input Data:", DateTime.UtcNow);
            OperationResponse OR = new OperationResponse();
            FoodRepository ER = new FoodRepository();
            try
            {
                _logger.LogInformation("Calling Getallfoodcaragories method at {Time}.", DateTime.UtcNow);
                OR = ER.Getfoodcatagories(0);
                _logger.LogInformation("Getallfoodcaragories completed successfully at {Time}.", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getallfoodcaragories at {Time}. Exception: {Message}. StackTrace: {StackTrace}", DateTime.UtcNow, ex.Message, ex.StackTrace);
            }
            _logger.LogInformation("End Getallfoodcaragories at {Time}.", DateTime.UtcNow);
            return OR;
        }
        [HttpGet]
        [Route("Getfooditem")]
        public OperationResponse Getfooditem(int Categoyid=0)
        {
            _logger.LogInformation("Started Getfooditem at {Time}. Input Data:", DateTime.UtcNow);
            OperationResponse OR = new OperationResponse();
            FoodRepository ER = new FoodRepository();
            try
            {
                _logger.LogInformation("Calling Getfooditem method at {Time}.", DateTime.UtcNow);
                OR = ER.Getfooditem(Categoyid);
                _logger.LogInformation("Getfooditem completed successfully at {Time}.", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getfooditem at {Time}. Exception: {Message}. StackTrace: {StackTrace}", DateTime.UtcNow, ex.Message, ex.StackTrace);
            }
            _logger.LogInformation("End Getfooditem at {Time}.", DateTime.UtcNow);
            return OR;
        }
        [HttpPost]
        [Route("UploadFoodImage")]
        #endregion
        public async Task<OperationResponse> UploadFoodImage(IFormFile image)
        {
            _logger.LogInformation("Started UploadFoodImage at {Time}. Input Data:", DateTime.UtcNow);
            OperationResponse OR = new OperationResponse();
            FoodRepository ER = new FoodRepository();
            try
            {
                if (image.Length > 10 * 1024 * 1024)
                {
                    OR.Status = 0;
                    OR.Message = "Original image cannot exceed 10 MB.";
                    return OR;
                }
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {

                    OR.Status = 0;
                    OR.Message = "Only JPG, JPEG, PNG and WEBP images are allowed.";
                    return OR;
                }
                _logger.LogInformation("Current Directory: {Path}", "D:\\RAHUL DHARA\\Test Project\\Food Application\\Frontend\\Roadsidebite\\");

                var uploadFolder= Path.Combine("D:\\RAHUL DHARA\\Test Project\\Food Application\\Frontend\\Roadsidebite\\", "public","images","Catagory");
                _logger.LogInformation("Upload Folder: {Path}", uploadFolder);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var imageName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadFolder, imageName);
                _logger.LogInformation("Full File Path: {Path}",filePath);
                // Save image
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await image.CopyToAsync(stream);
                //}
                
                using (var uploadedImage = await Image.LoadAsync(image.OpenReadStream()))
                {
                    // Resize while maintaining aspect ratio
                    uploadedImage.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(800, 800),
                        Mode = ResizeMode.Max
                    }));

                    byte[] compressedBytes = null;

                    // Reduce quality until image is 60 KB or less
                    for (int quality = 85; quality >= 10; quality -= 5)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await uploadedImage.SaveAsJpegAsync(
                                memoryStream,
                                new JpegEncoder { Quality = quality }
                            );

                            compressedBytes = memoryStream.ToArray();
                        }

                        if (compressedBytes.Length <= 60 * 1024)
                        {
                            break;
                        }
                    }

                    if (compressedBytes == null ||
                        compressedBytes.Length > 60 * 1024)
                    {
                        OR.Status = 0;
                        OR.Message = "Unable to compress image below 60 KB. Please select another image.";
                        return OR;
                    }

                    await System.IO.File.WriteAllBytesAsync(filePath, compressedBytes);
                }
                var imagePath = "/images/Catagory/" + imageName;
                OR.Status = 1;
                OR.Message = "Files uploaded successfully.";
                OR.Data = imagePath;
                _logger.LogInformation("Image saved successfully at: {FilePath}", filePath);
                return OR;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading food image");
                OR.Status = 0;
                OR.Message = "Error during file upload: " + ex.Message;
                return OR;
            }
            
        }
        [HttpPost]
        [Route("AddEditFoodCategory")]
        public OperationResponse AddEditFoodCategory(FoodCategory Fcdata)
        {
            _logger.LogInformation("Started AddEditFoodCategory at {Time}. Input Data:", DateTime.UtcNow);
            OperationResponse OR = new OperationResponse();
            FoodRepository ER = new FoodRepository();
            try
            {
                _logger.LogInformation("Calling AddEditFoodCategory method on FoodRepository at {Time}.", DateTime.UtcNow);
                OR = ER.AddEditFoodCategory(Fcdata);
                _logger.LogInformation("after  AddEditFoodCategory method on FoodRepository at {Time}. and status={Status} and maasage={Message}", DateTime.UtcNow,OR.Status,OR.Message);
            }
            catch (Exception ex)
            {
                OR.Status = 0;
                OR.Message = ex.Message;
                OR.Data = null;
                _logger.LogError(ex, "Error AddeditStudent at {Time}. Exception: {Message}. StackTrace: {StackTrace}", DateTime.UtcNow, ex.Message, ex.StackTrace);

            }
            _logger.LogInformation("End AddeditStudent at {Time}.", DateTime.UtcNow);
            return OR;
        }

       }
}
