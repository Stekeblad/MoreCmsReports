using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stekeblad.MoreCmsReports.DataModels
{
    public class SuperSearchFilterData
    {
        public Dictionary<string, ReflectedContentType> PageTypes { get; set; } = new Dictionary<string, ReflectedContentType>();
        public Dictionary<string, ReflectedContentType> BlockTypes { get; set; } = new Dictionary<string, ReflectedContentType>();
        public Dictionary<string, ReflectedContentType> InterfaceTypes { get; set; } = new Dictionary<string, ReflectedContentType>();
    }

    public class ReflectedContentType
    {
        public Type Type { get; set; }
        public bool FromEpi { get; set; }
        public Dictionary<string, ReflectedPropertyInfo> Properties { get; set; } = new Dictionary<string, ReflectedPropertyInfo>();
    }

    public class ReflectedPropertyInfo
    {
        public System.Reflection.PropertyInfo PropertyInfo { get; set; }
        public bool FromEpi { get; set; }
    }
}
