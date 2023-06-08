using NBomber.Contracts;
using NBomber.Contracts.Stats;

namespace NBomberFluentApi.Lib.Interfaces;

public interface IInjectScenario
{
    /// <summary>
    /// Injects a given number of scenario copies (threads) per 1 sec
    /// during a given duration. 
    /// Every single scenario copy will run only once.
    /// Use it when you want to maintain a constant rate of requests 
    /// without being affected by the performance of the system under test.
    /// </summary>
    /// <param name="injectRatePerSec"></param>
    /// <returns></returns>
    public IInjectScenario WithScenarioInjectRatePerSecond(int injectRatePerSec);

    /// <summary>
    /// How long a scenario should run
    /// </summary>
    /// <param name="durationInSeconds"></param>
    /// <returns></returns>
    public IInjectScenario WithScenarioDuration(int durationInSeconds);

    /// <summary>
    /// Set Scenario name that will be used in the report
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IInjectScenario WithScenarioName(string name);


    /// <summary>
    /// Build your scenario that can be use to append to another stress test process
    /// </summary>
    /// <returns></returns>
    public Scenario BuildScenario();

    /// <summary>
    /// This will run your stress test scenario and all of your appended scenarios.
    /// After your stress test has run it will generate a report based on your result.
    /// </summary>
    public NodeStats? Run();
}