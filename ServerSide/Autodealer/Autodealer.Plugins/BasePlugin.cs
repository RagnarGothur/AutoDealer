using Autodealer.Plugins.Extensions;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins
{
    /// <summary>
    /// Абстрактный класс плагина
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// Выполняет код плагина
        /// </summary>
        /// <param name="serviceProvider"></param>
        public abstract void Execute(IServiceProvider serviceProvider);

        /// <summary>
        /// Возвращает основные используемые сервисы из проводника сервисов
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Кортеж ITracingService, IPluginExecutionContext, IOrganizationService</returns>
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
