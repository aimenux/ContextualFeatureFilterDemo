using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace WebApi.Filters;

[FilterAlias("Terminal")]
public class TerminalFeatureFilter : IContextualFeatureFilter<TerminalContext>
{
    private readonly ILogger<TerminalFeatureFilter> _logger;

    public TerminalFeatureFilter(IOptions<TargetingEvaluationOptions> options, ILogger<TerminalFeatureFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext, TerminalContext appContext)
    {
        var settings = featureFilterContext.Parameters.Get<TerminalFeatureFilterSettings>();
        var runtimeContext = new TerminalContext(settings.TerminalType);
        var isEnabled = runtimeContext.Equals(appContext);
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{featureFilterContext.FeatureName}' is not enabled for current context '{appContext}'.");
        }
        return Task.FromResult(isEnabled);
    }
}