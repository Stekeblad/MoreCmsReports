using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
