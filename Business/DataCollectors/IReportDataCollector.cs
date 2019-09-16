using System;

namespace Stekeblad.MoreCmsReports.Business.DataCollectors
{
    /// <summary>
    /// Implementations of IReportDataCollector is discovered, instansiated and executed by the GenerateMoreCmsReportsData ScheduledJob.
    /// The first parameter to the Execute method is a reference to the job's stop signal and data collectors should regulary check
    /// if it have been set to true and if so abort execution.
    /// The second parameter is a method that calls the OnstatusChanged method in ScheduledJobBase and is used for sending status
    /// updated visible in the scheduled job admin interface
    /// </summary>
    public interface IReportDataCollector
    {
        string Execute(ref bool stopSignal, Func<string, bool> onStatusChanged);
    }
}
