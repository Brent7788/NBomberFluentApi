using Humanizer;
using NBomber.Contracts.Stats;
using NBomberFluentApi.Lib.Domain;

namespace NBomberFluentApi.Lib.Extensions;

public static class NodeStatsExtension
{
    public static List<StressTestData> ToStressTestData(this NodeStats? nodeStats)
    {
        var stressTestReportDetails = new List<StressTestData>();

        if (nodeStats is null) return stressTestReportDetails;

        foreach (var nodeStatsScenarioStat in nodeStats.ScenarioStats)
        {
            foreach (var stepStats in nodeStatsScenarioStat.StepStats)
            {
                stressTestReportDetails.Add(new StressTestData
                {
                    Scenario = nodeStatsScenarioStat.ScenarioName,
                    Duration = nodeStatsScenarioStat.Duration,
                    StepName = stepStats.StepName,
                    RequestCount = nodeStatsScenarioStat.RequestCount,
                    OkRequest = nodeStatsScenarioStat.OkCount,
                    FailedRequest = nodeStatsScenarioStat.FailCount,
                    RequestPerSecond = nodeStatsScenarioStat.RequestCount / nodeStatsScenarioStat.Duration.Seconds,
                    SmallestDataTransferredInKb = (stepStats.Ok.DataTransfer.MinBytes + stepStats.Fail.DataTransfer.MinBytes).Bytes().Kilobytes,
                    BiggestDataTransferredInKb = (stepStats.Ok.DataTransfer.MaxBytes + stepStats.Fail.DataTransfer.MaxBytes).Bytes().Kilobytes,
                    TotalDataTransferredInMb = nodeStatsScenarioStat.AllBytes.Bytes().Megabytes
                });
            }
        }

        return stressTestReportDetails;
    }
}