using NBomberFluentApi.Lib;
using Xunit.Abstractions;

namespace NBomberFluentApi.Test;

[Collection("Test Controller Collection")]
public class WeatherForecastStressTests
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _outputHelper;

    public WeatherForecastStressTests(
        ApplicationFactory<Program> applicationFactory,
        ITestOutputHelper outputHelper
    )
    {
        _client = applicationFactory.HttpClient;
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var r = await _client.GetAsync("WeatherForecast");

        var l = await r.Content.ReadAsStringAsync();
        Console.WriteLine($"Is this working ===================== {l}");
        _outputHelper.WriteLine("First test==========================");

        var s = StressTestFluentApi
            .Start()
            .SetHttpClient(_client)
            .SetUrl("WeatherForecast")
            .GetMethod()
            .InjectPerSecond()
            .WithScenarioInjectRatePerSecond(1)
            .WithScenarioDuration(3)
            .Run();

        Console.WriteLine($"Is this working ===================== {s}");
    }
}
