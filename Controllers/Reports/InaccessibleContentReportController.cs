using EPiServer.PlugIn;
using Stekeblad.MoreCmsReports.Business;
using Stekeblad.MoreCmsReports.DataModels;
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
    [Authorize(Roles = "CmsAdmins, Administrators, WebAdmins, CmsEditors, WebEditors")]
    public class InaccessibleContentReportController : Controller
    {
        public ActionResult Index()
        {
            InaccessibleContentLocatorData model = DataStorage.ReadObjectFromFile<InaccessibleContentLocatorData>();

            return View(model);
        }
    }
}
