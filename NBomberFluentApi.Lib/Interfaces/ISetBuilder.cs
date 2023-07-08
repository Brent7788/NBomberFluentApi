using NBomber.Contracts;

namespace NBomberFluentApi.Lib.Interfaces;

public interface ISetBuilder
{
    /// <summary>
    /// Append pre-build stress test scenario to a current build process.
    /// Note* These appended scenarios will only run at the end when call .Run()
    /// </summary>
    /// <param name="scenario"></param>
    /// <returns></returns>
    public ISetBuilder AppendScenario(Scenario scenario);

    /// <summary>
    /// Append pre-build stress test scenario to a current build process.
    /// Note* These appended scenarios will only run at the end when call .Run()
    /// </summary>
    /// <param name="scenarios"></param>
    /// <returns></returns>
    public ISetBuilder AppendScenario(params Scenario[] scenarios);

    /// <summary>
    /// By default HttpClient is initialize. Set HttpClient with existing client. 
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns></returns>
    public ISetBuilder SetHttpClient(HttpClient httpClient);

    /// <summary>
    /// Set HTTP Client Url for the current stress test scenario
    /// Note* This will not effect appended scenarios
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public IHttpMethod SetUrl(string url);
}
