using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Stekeblad.MoreCmsReports.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stekeblad.MoreCmsReports.Business.DataCollectors
{
    public class PagesNotVisibleInMenus : IReportDataCollector
    {
        public string Execute(ref bool stopSignal, Func<string, bool> onStatusChanged)
        {
            if (stopSignal)
                return $"Execution of {this.GetType().Name} was aborted by stop signal";

            onStatusChanged("Running PagesNotVisibleInMenus...");

            var repository = ServiceLocator.Current.GetInstance<IPageCriteriaQueryService>();

            PropertyCriteriaCollection criterias = new PropertyCriteriaCollection()
            {
                new PropertyCriteria()
                {
                    Name = MetaDataProperties.PageVisibleInMenu,
                    Type = PropertyDataType.Boolean,
                    Condition = EPiServer.Filters.CompareCondition.Equal,
                    Value = "false"
                }
            };

            PageDataCollection pages = repository.FindPagesWithCriteria(ContentReference.StartPage, criterias);
            PagesNotVisibleInMenusData pageNameList = new PagesNotVisibleInMenusData();
            foreach (PageData page in pages)
            {
                //if (page.PageTypeName.Equals(nameof(SlideShowPicturePage)) || page.PageTypeName.Equals(nameof(ContactPage)) || page.PageTypeName.Equals(nameof(SortingItemPage)))
                //    continue;
                pageNameList.Pages.Add(new ContentReportItemWithType() {
                    PageContentLinkId = page.ContentLink.ID,
                    PageName = page.Name,
                    PageTypeName = page.PageTypeName
                });
            }

            DataStorage.WriteObjectToFile<PagesNotVisibleInMenusData>(pageNameList);
            return "I am done, doobie damm damm";
        }
    }
}
