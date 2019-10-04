using EPiServer.Core;
using EPiServer.PlugIn;
using Stekeblad.MoreCmsReports.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stekeblad.MoreCmsReports.Controllers.Plugin
{
    [GuiPlugIn(
        Area = PlugInArea.AdminConfigMenu,
        UrlFromModuleFolder = "SuperSearchPlugin",
        DisplayName = "MoreCmsReports SuperSearch"
    )]
    [Authorize(Roles = "CmsAdmins, Administrators, WebAdmins")]
    public class SuperSearchPluginController : Controller
    {
        public ActionResult Index()
        {
            SuperSearchFilterData model = GetSearchableContentTypes();
            return View(model);
        }

        private SuperSearchFilterData GetSearchableContentTypes()
        {
            SuperSearchFilterData filterData = new SuperSearchFilterData();

            Assembly targetAssembly = Assembly.Load("PureAlloyMvc");

            List<Type> semiFilteredTypes = targetAssembly.GetTypes().Where(type => type.IsClass).ToList();

            // Find all types inheriting from PageData and all their members

            List<Type> pageDataTypes = semiFilteredTypes.Where(t => t.IsSubclassOf(typeof(PageData))).ToList();
            foreach (Type type in pageDataTypes)
            {
                ReflectedContentType reflectedContent = new ReflectedContentType()
                {
                    Type = type,
                    FromEpi = false // No class in episerver inherits from PageData
                };
                foreach (PropertyInfo property in type.GetProperties())
                {
                    ReflectedPropertyInfo reflectedProperty = new ReflectedPropertyInfo()
                    {
                        PropertyInfo = property,
                        FromEpi = typeof(PageData).GetProperty(property.Name) != null // if the property is defined in PageData or on an even lower level
                    };
                    reflectedContent.Properties.Add(property.Name, reflectedProperty);
                }
                filterData.PageTypes.Add(type.Name, reflectedContent);
            }

            // Find all types inheriting from BlockData and all their members

            List<Type> blockDataTypes = semiFilteredTypes.Where(t => t.IsSubclassOf(typeof(BlockData))).ToList();
            foreach (Type type in blockDataTypes)
            {
                ReflectedContentType reflectedContent = new ReflectedContentType()
                {
                    Type = type,
                    FromEpi = false // No class in episerver inherits BlockData
                };
                foreach (PropertyInfo property in type.GetProperties())
                {
                    ReflectedPropertyInfo reflectedProperty = new ReflectedPropertyInfo()
                    {
                        PropertyInfo = property,
                        FromEpi = typeof(BlockData).GetProperty(property.Name) != null // if the property is defined in BlockData or on an even lower level
                    };
                    reflectedContent.Properties.Add(property.Name, reflectedProperty);
                }
                filterData.BlockTypes.Add(type.Name, reflectedContent);
            }

            // Find all interfaces that is implemented for any of the found types

            List<Type> blocksAndPages = new List<Type>();
            blocksAndPages.AddRange(pageDataTypes);
            blocksAndPages.AddRange(blockDataTypes);
            foreach (Type type in blocksAndPages.SelectMany(type => type.GetInterfaces()).Distinct()) // make sure to remove duplicates, or else it will be alot of them
            {
                if (filterData.InterfaceTypes.Keys.Contains(type.Name))
                    continue;

                ReflectedContentType reflectedContent = new ReflectedContentType()
                {
                    Type = type,
                    FromEpi = type.Assembly.FullName.StartsWith("EPiServer") // EPiServer have many interfaces on pages and blocks
                };
                foreach (PropertyInfo property in type.GetProperties())
                {
                    ReflectedPropertyInfo reflectedProperty = new ReflectedPropertyInfo()
                    {
                        PropertyInfo = property,
                        FromEpi = reflectedContent.FromEpi // if the property is in an interface from episerver, the property is from episerver
                    };
                    reflectedContent.Properties.Add(property.Name, reflectedProperty);
                }
                filterData.InterfaceTypes.Add(type.Name, reflectedContent);
            }

            return filterData;
        }
    }
}
