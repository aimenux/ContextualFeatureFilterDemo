using ConsoleApp.Filters;
using ConsoleApp.Models;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace ConsoleApp.Services;

public interface IFeatureService
{
    Task<FeaturesResponse> GetContextualFeaturesAsync(CancellationToken cancellationToken = default);
}

public class FeatureService : IFeatureService
{
    private readonly IFeatureManager _featureManager;

    public FeatureService(IFeatureManager featureManager)
    {
        _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
    }

    public async Task<FeaturesResponse> GetContextualFeaturesAsync(CancellationToken cancellationToken)
    {
        var responses = new FeaturesResponse();

        var users = new List<User>
        {
            new User("Julia"),
            new User("Aadam"),
            new User("Abdul", "HR"),
            new User("Irene", "IT")
        };

        var targetingContexts = users.Select(x => new TargetingContext
        {
            UserId = x.UserId,
            Groups = x.Groups
        });

        foreach (var targetingContext in targetingContexts)
        {
            const string featureName = nameof(FeatureFlags.FeatureA);
            var isEnabled = await _featureManager.IsEnabledAsync(featureName, targetingContext);
            responses.Add(new FeatureResponse
            {
                FeatureName = featureName,
                FeatureContext = targetingContext,
                IsEnabled = isEnabled
            });
        }

        var terminals = new List<Terminal>
        {
            new Terminal("Pc"),
            new Terminal("Mac"),
            new Terminal("Mobile")
        };

        var terminalContexts = terminals.Select(x => new TerminalContext(x.TerminalType));

        foreach (var terminalContext in terminalContexts)
        {
            const string featureName = nameof(FeatureFlags.FeatureB);
            var isEnabled = await _featureManager.IsEnabledAsync(featureName, terminalContext);
            responses.Add(new FeatureResponse
            {
                FeatureName = featureName,
                FeatureContext = terminalContext,
                IsEnabled = isEnabled
            });
        }

        return responses;
    }
}