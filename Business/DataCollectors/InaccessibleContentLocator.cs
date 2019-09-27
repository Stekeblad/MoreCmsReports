using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using Stekeblad.MoreCmsReports.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stekeblad.MoreCmsReports.Business.DataCollectors
{
    public class InaccessibleContentLocator : IReportDataCollector
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IContentModelUsage _contentModelUsage;
        private readonly IContentRepository _contentRepository;

        public InaccessibleContentLocator()
        {
            _contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            _contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();
            _contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
        }

        public string Execute(ref bool stopSignal, Func<string, bool> onStatusChanged)
        {
            if (stopSignal)
                return $"Execution of {this.GetType().Name} was aborted by stop signal";

            InaccessibleContentLocatorData results = new InaccessibleContentLocatorData();

            foreach (ContentType tajp in _contentTypeRepository.List()) // get all page/block types
            {
                if (stopSignal)
                    return "aborted";

                onStatusChanged($"Counting blocks and pages of type {tajp.Name}...");

                // Exclude system types
                if (tajp.Name.StartsWith("Sys"))
                    continue;

                if (!_contentModelUsage.IsContentTypeUsed(tajp))
                {
                    // not used at all
                    results.TypesOverview.Add(new TypeUsageCount() {
                        TypeName = tajp.Name,
                        UnfilteredUsages = 0,
                        UnavailableUsages = 0
                    });
                }
                else
                {
                    IEnumerable<IContent> allUsages = _contentModelUsage.ListContentOfContentType(tajp) // get all usages
                        .Select(usage => _contentRepository.Get<IContent>(usage.ContentLink.ToReferenceWithoutVersion())) // pick latest version
                        .Distinct(); // exclude duplicates

                    int usagesBeforeVisitorFilter = allUsages.Count(); // count all existing pages/blocks

                    if (allUsages.First() is BlockData || allUsages.First() is MediaData)
                    {
                        List<IContent> allUsagesList = allUsages.ToList();
                        new FilterPublished().Filter(allUsagesList);
                        int unavailabeUsages = 0;
                        foreach (IContent block in allUsagesList) // check every block if it is used anywhere and if the pages/blocks its used in is used anywhere (recursive)
                        {
                            IEnumerable<ReferenceInformation> usedOn = _contentRepository.GetReferencesToContent(block.ContentLink, false);
                            if (!usedOn.Any() || !usedOn.Any(usage => IsBlockVisibleToVisitor(usage.OwnerID)))
                            {
                                // Block is not used or not visible, record it
                                unavailabeUsages++;
                                if (!results.TypeDetails.ContainsKey(tajp.Name))
                                    results.TypeDetails.Add(tajp.Name, new List<ContentReportItem>());
                                results.TypeDetails[tajp.Name].Add(new ContentReportItem()
                                {
                                    ContentLinkId = block.ContentLink.ID,
                                    ContentName = block.Name
                                });
                            }
                        }

                        results.TypesOverview.Add(new TypeUsageCount()
                        {
                            TypeName = tajp.Name,
                            UnfilteredUsages = usagesBeforeVisitorFilter,
                            UnavailableUsages = unavailabeUsages
                        });
                    }
                    else if (allUsages.First() is PageData)
                    {
                        // remove all pages from allUsages that passes the FilterForVisitors filter
                        IEnumerable<IContent> allFilteredUsages = allUsages.Except(FilterForVisitor.Filter(allUsages));
                        int unavailableUsages = allFilteredUsages.Count(); // recount
                        results.TypesOverview.Add(new TypeUsageCount()
                        {
                            TypeName = tajp.Name,
                            UnfilteredUsages = usagesBeforeVisitorFilter,
                            UnavailableUsages = unavailableUsages
                        });
                        foreach (IContent unusedContent in allFilteredUsages)
                        {
                            if (!results.TypeDetails.ContainsKey(tajp.Name))
                                results.TypeDetails.Add(tajp.Name, new List<ContentReportItem>());
                            results.TypeDetails[tajp.Name].Add(new ContentReportItem()
                            {
                                ContentLinkId = unusedContent.ContentLink.ID,
                                ContentName = unusedContent.Name
                            });
                        }
                    }
                }
            }
            DataStorage.WriteObjectToFile<InaccessibleContentLocatorData>(results);
            return "I am done, doobie damm damm";
        }

        private bool IsBlockVisibleToVisitor(ContentReference contRef)
        {
            // TODO potential optimization, cache block ID and if it is visible true/false, can have big effect when high reuse and deep nesting of blocks
            if (ContentReference.IsNullOrEmpty(contRef))
                return false;

            IContent content = _contentRepository.Get<IContent>(contRef);
            if (content is PageData page) // is block on a page
            {
                PageDataCollection pdc = new PageDataCollection
                {
                      page
                  };
                var filtered = FilterForVisitor.Filter(pdc);
                return filtered.Count > 0; // is page published?
            }
            else if (content is BlockData) // is block in other block
            {
                IEnumerable<ReferenceInformation> usedOn = _contentRepository.GetReferencesToContent(content.ContentLink, false);
                return usedOn.Any() && usedOn.Any(usage => IsBlockVisibleToVisitor(usage.OwnerID)); // check all usages of that block
            }
            else
            {
                return false;
            }
        }
    }
}
