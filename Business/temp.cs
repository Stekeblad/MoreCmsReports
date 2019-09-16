using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Filters;
using System;
using EPiServer.Framework.Localization;
using System.Globalization;

namespace Stekeblad.CmsAnalysisThings
{
    [ScheduledPlugIn(DisplayName = "TypesCounter")]
    public class TypesCounter : ScheduledJobBase
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IContentModelUsage _contentModelUsage;
        private readonly IContentRepository _contentRepository;
        private readonly LocalizationService _localizationService;

        public TypesCounter()
        {
            IsStoppable = false;

            _contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            _contentModelUsage = ServiceLocator.Current.GetInstance<IContentModelUsage>();
            _contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            _localizationService = LocalizationService.Current;
        }

        public override string Execute()
        {
            bool stringExists = _localizationService.TryGetStringByCulture("contenttypes/teaserblock/name", CultureInfo.CurrentUICulture, out string localizedString);

            return "";
        }
    }
}