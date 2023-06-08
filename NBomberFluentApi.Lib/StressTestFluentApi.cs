using System.Net.Http.Json;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomberFluentApi.Lib.Interfaces;
using Serilog;

namespace NBomberFluentApi.Lib;

public class StressTestFluentApi : ISetBuilder, IHttpMethod, IHttpSetBody, ILoadSimulation, IConstantScenario,
    IInjectScenario
{
    private HttpClient _httpClient = new();
    private HttpMethod HttpMethod { get; set; } = null!;
    private string Url { get; set; } = null!;
    private object HttpBody { get; set; } = new { };
    private int ScenarioCopiesRates { get; set; } = Faker.RandomNumber.Next(1, 20);
    private TimeSpan ScenarioDuration { get; set; } = TimeSpan.FromSeconds(Faker.RandomNumber.Next(5, 50));
    private string ScenarioName { get; set; } = $"Noname_{Faker.RandomNumber.Next(1000, 9999)}";
    private List<Scenario> Scenarios { get; }

    private StressTestFluentApi()
    {
        Scenarios = new List<Scenario>();
    }

    /// <summary>
    /// Initialize Stress Test Fluent Api
    /// </summary>
    /// <returns></returns>
    public static ISetBuilder Start() => new StressTestFluentApi();

    public ISetBuilder AppendScenario(Scenario scenario)
    {
        if (scenario is null)
            throw new ArgumentNullException(nameof(scenario), "Cannot append null scenario");

        Scenarios.Add(scenario);
        return this;
    }

    public ISetBuilder AppendScenario(params Scenario[] scenarios)
    {
        if (scenarios is null || !scenarios.Any())
            throw new ArgumentNullException(nameof(scenarios), "Cannot append empty or null scenarios");

        Scenarios.AddRange(scenarios);
        return this;
    }

    public ISetBuilder SetHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "Http Client cannot be null");
        return this;
    }

    public IHttpMethod SetUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            throw new ArgumentNullException(nameof(url), "Please provide url");

        Url = url;
        return this;
    }

    public ILoadSimulation GetMethod()
    {
        HttpMethod = HttpMethod.Get;
        return this;
    }

    public IHttpSetBody PostMethod()
    {
        HttpMethod = HttpMethod.Post;
        return this;
    }

    public IHttpSetBody PutMethod()
    {
        HttpMethod = HttpMethod.Put;
        return this;
    }

    public ILoadSimulation SetBody(object obj)
    {
        HttpBody = obj;
        return this;
    }

    public ILoadSimulation UseGenericBody()
    {
        HttpBody = new
        {
            testArgOne = "testing",
            testArgTwo = 7
        };
        return this;
    }

    public ILoadSimulation UseEmptyBody()
    {
        return this;
    }

    public IConstantScenario KeepConstant()
    {
        return this;
    }

    public IInjectScenario InjectPerSecond()
    {
        return this;
    }

    public IConstantScenario WithScenarioThreadCount(int count)
    {
        ScenarioCopiesRates = count;
        return this;
    }

    public IInjectScenario WithScenarioInjectRatePerSecond(int injectRatePerSec)
    {
        ScenarioCopiesRates = injectRatePerSec;
        return this;
    }

    IInjectScenario IInjectScenario.WithScenarioDuration(int durationInSeconds)
    {
        ScenarioDuration = TimeSpan.FromSeconds(durationInSeconds);
        return this;
    }

    IInjectScenario IInjectScenario.WithScenarioName(string name)
    {
        ScenarioName = name;
        return this;
    }


    public IConstantScenario WithScenarioDuration(int durationInSeconds)
    {
        ScenarioDuration = TimeSpan.FromSeconds(durationInSeconds);
        return this;
    }

    public IConstantScenario WithScenarioName(string name)
    {
        ScenarioName = name;
        return this;
    }

    Scenario IInjectScenario.BuildScenario()
    {
        return CreateInjectPerSecScenario();
    }

    Scenario IConstantScenario.BuildScenario()
    {
        return CreateKeepConstantScenario();
    }

    NodeStats? IInjectScenario.Run()
    {
        Scenarios.Add(CreateInjectPerSecScenario());
        return CreateNBomberRunner();
    }

    NodeStats? IConstantScenario.Run()
    {
        Scenarios.Add(CreateKeepConstantScenario());
        return CreateNBomberRunner();
    }

    private NodeStats? CreateNBomberRunner()
    {
        return NBomberRunner
            .RegisterScenarios(Scenarios.ToArray())
            .WithLoggerConfig(() => new LoggerConfiguration().MinimumLevel.Error())
            .WithoutReports()
            .WithDefaultStepTimeout(TimeSpan.FromSeconds(ScenarioDuration.Seconds / 2F))
            .Run();
    }

    private Scenario CreateKeepConstantScenario()
    {
        var step = CreateStep();

        var scenario = ScenarioBuilder
            .CreateScenario(ScenarioName, step)
            .WithoutWarmUp()
            .WithLoadSimulations(LoadSimulation.NewKeepConstant(ScenarioCopiesRates, ScenarioDuration));

        return scenario;
    }

    private Scenario CreateInjectPerSecScenario()
    {
        var step = CreateStep();

        var scenario = ScenarioBuilder
            .CreateScenario(ScenarioName, step)
            .WithoutWarmUp()
            .WithLoadSimulations(LoadSimulation.NewInjectPerSec(ScenarioCopiesRates, ScenarioDuration));

        return scenario;
    }

    private IStep CreateStep()
    {
        return Step.Create($"step {ScenarioName}", async _ =>
        {
            var requestMessage = new HttpRequestMessage(HttpMethod, Url);

            requestMessage.Content = JsonContent.Create(HttpBody);

            var response = await _httpClient.SendAsync(requestMessage);

            return response.IsSuccessStatusCode
                ? await HandleOkResponse(response)
                : await HandleFailResponse(response);
        });
    }

    private static async Task<Response> HandleFailResponse(HttpResponseMessage resp)
    {
        var jsonResp = await resp.Content.ReadAsStringAsync();
        var sizeBytes = resp.Content.Headers.ContentLength ?? 0;

        return Response.Fail(jsonResp, (int)resp.StatusCode, Convert.ToInt32(sizeBytes));
    }

    private static async Task<Response> HandleOkResponse(HttpResponseMessage resp)
    {
        var statusCode = (int)resp.StatusCode;
        var sizeBytes = resp.Content.Headers.ContentLength ?? 0;
        var json = await resp.Content.ReadAsStringAsync();

        return Response.Ok(json, statusCode, Convert.ToInt32(sizeBytes));
    }
}