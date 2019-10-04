using System.Collections.Generic;

namespace Stekeblad.MoreCmsReports.DataModels
{
    public class PagesNotVisibleInMenusData
    {
        public PagesNotVisibleInMenusData()
        {
            Pages = new List<ContentReportItemWithType>();
        }

        public List<ContentReportItemWithType> Pages { get; set; }
    }
}
