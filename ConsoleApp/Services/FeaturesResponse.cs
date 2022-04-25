namespace ConsoleApp.Services;

public class FeaturesResponse : List<FeatureResponse>
{
}

public class FeatureResponse
{
    public string FeatureName { get; set; }

    public object FeatureContext { get; set; }

    public bool IsEnabled { get; set; }
}