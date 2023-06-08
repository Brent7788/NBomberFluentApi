using NBomber.Contracts;
using NBomber.Contracts.Stats;

namespace NBomberFluentApi.Lib.Interfaces;

public interface IConstantScenario 
{
    /// <summary>
    /// A fixed number of scenario copies (threads) executes as many iterations
    /// as possible for a specified amount of time.
    /// Every single scenario copy will iterate while the specified duration.
    /// Use it when you need to run a specific amount of scenario copies (threads)
    /// for a certain amount of time.  
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public IConstantScenario WithScenarioThreadCount(int count);

    /// <summary>
    /// How long a scenario should run
    /// </summary>
    /// <param name="durationInSeconds"></param>
    /// <returns></returns>
    public IConstantScenario WithScenarioDuration(int durationInSeconds);

    /// <summary>
    /// Set Scenario name that will be used in the report
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IConstantScenario WithScenarioName(string name);

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