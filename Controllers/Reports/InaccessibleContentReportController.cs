using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stekeblad.MoreCmsReports.Controllers.Reports
{
    [GuiPlugIn(
        Area = PlugInArea.ReportMenu,
        Category = "MoreCmsReports",
        DisplayName = "Inaccessible Content",
        UrlFromModuleFolder = "InaccessibleContentReport"
        )]
    public class InaccessibleContentReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
