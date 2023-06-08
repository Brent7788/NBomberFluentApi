namespace NBomberFluentApi.Lib.Interfaces;

public interface ILoadSimulation
{
    /// <summary>
    /// A fixed number of scenario copies (threads) executes as many iterations
    /// as possible for a specified amount of time.
    /// Every single scenario copy will iterate while the specified duration.
    /// Use it when you need to run a specific amount of scenario copies (threads)
    /// for a certain amount of time.  
    /// </summary>
    /// <returns></returns>
    public IConstantScenario KeepConstant();

    /// <summary>
    /// Injects a given number of scenario copies (threads) per 1 sec
    /// during a given duration. 
    /// Every single scenario copy will run only once.
    /// Use it when you want to maintain a constant rate of requests 
    /// without being affected by the performance of the system under test.
    /// </summary>
    /// <returns></returns>
    public IInjectScenario InjectPerSecond();
}