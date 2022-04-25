using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly ILogger<FeatureController> _logger;

        public FeatureController(IFeatureService featureService, ILogger<FeatureController> logger)
        {
            _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet(Name = "GetFeatures")]
        public async Task<IActionResult> GetFeaturesAsync(CancellationToken cancellationToken = default)
        {
            var responses = await _featureService.GetContextualFeaturesAsync(cancellationToken);
            return new OkObjectResult(new
            {
                Features = responses
            });
        }
    }
}