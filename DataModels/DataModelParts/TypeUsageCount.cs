using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stekeblad.MoreCmsReports.DataModels
{

    public class TypeUsageCount
    {
        public string TypeName { get; set; }
        public int UnfilteredUsages { get; set; }
        public int UnavailableUsages { get; set; }
    }
}
