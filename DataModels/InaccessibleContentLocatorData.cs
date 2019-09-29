using System.Collections.Generic;

namespace Stekeblad.MoreCmsReports.DataModels
{
    public class InaccessibleContentLocatorData
    {
        public List<TypeAccessabilityReport> TypesWithNoUsages { get; set; }
        public List<TypeAccessabilityReport> TypesWithInaccessibleContent { get; set; }
        public List<TypeAccessabilityReport> TypesWithoutIssues { get; set; }

        public InaccessibleContentLocatorData()
        {
            TypesWithNoUsages = new List<TypeAccessabilityReport>();
            TypesWithoutIssues = new List<TypeAccessabilityReport>();
            TypesWithInaccessibleContent = new List<TypeAccessabilityReport>();
        }
    }

    public class TypeAccessabilityReport
    {
        public string TypeName { get; set; }
        public int TotalUsages { get; set; }
        public int InaccessibleUsages { get; set; }
        public List<ContentReportItem> Usages { get; set; }

        public TypeAccessabilityReport()
        {
            Usages = new List<ContentReportItem>();
        }
    }
}
