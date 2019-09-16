using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Stekeblad.MoreCmsReports.Controllers.Plugin
{
    [GuiPlugIn(
        Area = PlugInArea.AdminConfigMenu,
        //UrlFromModuleFolder = "Views/MoreCmsReportsControlPlugin/Index",
        UrlFromModuleFolder = "MoreCmsReportsControlPlugin",
        DisplayName = "Configure More Cms Reports"
    )]
    //[Authorize(Roles = "CmsAdmins, Administrators, WebAdmins")]
    public class MoreCmsReportsCPluginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
