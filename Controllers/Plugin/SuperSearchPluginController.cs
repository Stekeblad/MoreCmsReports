using EPiServer.Core;
using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stekeblad.MoreCmsReports.Controllers.Plugin
{
    //[GuiPlugIn(
    //    Area = PlugInArea.AdminConfigMenu,
    //    //UrlFromModuleFolder = "Views/MoreCmsReportsControlPlugin/Index",
    //    //UrlFromModuleFolder = "MoreCmsReportsControlPlugin",
    //    DisplayName = "MoreCmsReports SuperSearch"
    //)]
    ////[Authorize(Roles = "CmsAdmins, Administrators, WebAdmins")]
    //public class SuperSearchPluginController : Controller
    //{
    //    public ActionResult Index()
    //    {
    //        GetSearchableContentTypes();
    //        return View();
    //    }

    //    private void GetSearchableContentTypes()
    //    {
    //        var pagetypes = GetEnumerableOfType<PageData>();
    //        var blocktypes = GetEnumerableOfType<BlockData>();
    //        // Find interfaces is some way?
    //        throw new NotImplementedException();
    //    }

    //    private IEnumerable<T> GetEnumerableOfType<T>()
    //    {
    //        List<T> objects = new List<T>();
    //        foreach (Type type in
    //            Assembly.GetAssembly(typeof(T)).GetTypes()
    //            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
    //        {
    //            objects.Add((T)Activator.CreateInstance(type));
    //        }
    //        return objects;
    //    }
    //}
}
