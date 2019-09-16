using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using Stekeblad.MoreCmsReports.Business.DataCollectors;

namespace Stekeblad.MoreCmsReports.Business.ScheduledJobs
{
    [ScheduledPlugIn(DisplayName = "GenerateMoreCmsReportsData")]
    public class GenerateMoreCmsReportsData : ScheduledJobBase
    {
        private bool _stopSignaled;

        public GenerateMoreCmsReportsData()
        {
            IsStoppable = true;
        }

        public override void Stop()
        {
            _stopSignaled = true;
        }

        public bool PostStatusUpdate(string message)
        {
            OnStatusChanged(message);
            return true;
        }

        public override string Execute()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> jobReportMessage = new List<string>();

            List<TypeInfo> dataCollectors = GetAllDataCollectors();

            OnStatusChanged($"Starting... {dataCollectors.Count} enabled data collector(s) found");

            foreach (TypeInfo typeInfo in dataCollectors)
            {
                if (_stopSignaled)
                {
                    stopwatch.Stop();
                    jobReportMessage.Add($"[{stopwatch.Elapsed.ToString("mm\\:ss")}] Stop of job was called");
                    return string.Join("<br />", jobReportMessage);
                }

                // Instansiate and execute ReportDataCollector
                IReportDataCollector dataCollector = (IReportDataCollector)Activator.CreateInstance(typeInfo.AsType());
                jobReportMessage.Add($"[{stopwatch.Elapsed.ToString("mm\\:ss")}]) Starting data collector: {typeInfo.Name}");
                string result = dataCollector.Execute(ref _stopSignaled, PostStatusUpdate);
                jobReportMessage.Add($"[{typeInfo.Name}] {result}");
            }

            stopwatch.Stop();
            jobReportMessage.Add($"[{stopwatch.Elapsed.ToString("mm\\:ss")}] Job finished");
            return string.Join("<br />", jobReportMessage);
        }

        private List<TypeInfo> GetAllDataCollectors()
        {
            // TODO allow individual collectors to be toggled on/off and exclude them here
            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            return currentAssembly.DefinedTypes.Where(typeInfo => typeInfo.ImplementedInterfaces.Any(type => type == typeof(IReportDataCollector))).ToList();
        }
    }
}
