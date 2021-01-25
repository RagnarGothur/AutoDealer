using Autodealer.Plugins.Extensions;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        public abstract void Execute(IServiceProvider serviceProvider);

        protected static (ITracingService, IPluginExecutionContext, IOrganizationService) GetMainServices(IServiceProvider serviceProvider)
        {
            var tracingService = serviceProvider.GetService<ITracingService>();

            tracingService.Trace(
                $"Get {nameof(IPluginExecutionContext)}, {nameof(IOrganizationServiceFactory)}, {nameof(IOrganizationService)}"
            );

            var context = serviceProvider.GetService<IPluginExecutionContext>();
            var crmFactory = serviceProvider.GetService<IOrganizationServiceFactory>();

            tracingService.Trace($"Create {nameof(IOrganizationService)}");
            var organizationService = crmFactory.CreateOrganizationService(Guid.Empty);

            return (tracingService, context, organizationService);
        }
    }
}
