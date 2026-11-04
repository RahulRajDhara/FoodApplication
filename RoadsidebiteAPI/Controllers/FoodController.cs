using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
