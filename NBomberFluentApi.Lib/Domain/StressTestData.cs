namespace NBomberFluentApi.Lib.Domain;

public class StressTestData
{
   public Guid Id { get; set; }
   public string Scenario { get; set; } = null!;
   public string ScenarioUrl { get; set; } = null!;
   public TimeSpan Duration { get; set; }
   public string StepName { get; set; } = null!;
   public int RequestCount { get; set; }
   public int OkRequest { get; set; }
   public int FailedRequest { get; set; }
   public int RequestPerSecond { get; set; }
   public double SmallestDataTransferredInKb { get; set; }
   public double BiggestDataTransferredInKb { get; set; }
   public double TotalDataTransferredInMb { get; set; }
}