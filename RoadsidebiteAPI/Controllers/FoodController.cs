using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

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
        #endregion

        }
}
