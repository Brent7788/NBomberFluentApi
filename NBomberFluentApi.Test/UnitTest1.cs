using Xunit.Abstractions;

namespace NBomberFluentApi.Test;

[Collection("Test Controller Collection")]
public class UnitTest1
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ApplicationFactory<Program> applicationFactory, ITestOutputHelper outputHelper)
    {
        _client = applicationFactory.HttpClient;
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var r = await _client.GetAsync("WeatherForecast");
        _outputHelper.WriteLine("First test==========================");
    }
}
