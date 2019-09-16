using EPiServer.PlugIn;
using Stekeblad.MoreCmsReports.Business;
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
        DisplayName = "Configure MoreCmsReports"
    )]
    //[Authorize(Roles = "CmsAdmins, Administrators, WebAdmins")]
    public class MoreCmsReportsPluginController : Controller
    {
        public ActionResult Index()
        {
            // Try to see if method is executed or errors accure before that happens
            DataStorage.WriteObjectToFile<MoreCmsReportsPluginController>($"The Index method of {nameof(MoreCmsReportsPluginController)} was successfully invoked");
            return View();
        }
    }
}
