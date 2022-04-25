using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace ConsoleApp.Filters;

[FilterAlias("Terminal")]
public class TerminalFeatureFilter : IContextualFeatureFilter<TerminalContext>
{
    private readonly IOptions<TargetingEvaluationOptions> _options;
    private readonly ILogger<TerminalFeatureFilter> _logger;

    public TerminalFeatureFilter(IOptions<TargetingEvaluationOptions> options, ILogger<TerminalFeatureFilter> logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
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