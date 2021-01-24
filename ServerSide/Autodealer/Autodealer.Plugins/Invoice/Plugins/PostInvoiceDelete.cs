using Autodealer.Plugins.Invoice.Handlers;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins.Invoice.Plugins
{
    public class PostInvoiceDelete : BasePlugin, IPlugin
    {
        public override void Execute(IServiceProvider serviceProvider)
        {
            var (tracer, ctx, crm) = GetMainServices(serviceProvider);

            try
            {
                tracer.Trace($"Get {nameof(Entity)}");
                var target = ((EntityReference)ctx.InputParameters["Target"]);

                tracer.Trace($"Create {nameof(InvoiceHandler)}");
                var service = new InvoiceHandler(crm, tracer);

                service.HandlePostDelete(target);
            }
            catch (Exception e)
            {
                tracer.Trace($"Exception catched: {e.GetType()} - {e.Message}");
                tracer.Trace(e.StackTrace);
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
