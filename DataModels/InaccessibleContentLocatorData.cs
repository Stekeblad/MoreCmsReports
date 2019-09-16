using System.Collections.Generic;

namespace Stekeblad.MoreCmsReports.DataModels
{
    public class InaccessibleContentLocatorData
    {
        public InaccessibleContentLocatorData()
        {
            TypesOverview = new List<TypeUsageCount>();
            TypeDetails = new Dictionary<string, List<ContentReportItem>>();
        }

        public List<TypeUsageCount> TypesOverview { get; set; }
        public Dictionary<string, List<ContentReportItem>> TypeDetails { get; set; }
    }
}
