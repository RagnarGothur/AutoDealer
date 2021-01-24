using Autodealer.Plugins.Agreement.Handlers;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins.Agreement.Plugins
{
    public class PostAgreementCreate : BasePlugin, IPlugin
    {
        public override void Execute(IServiceProvider serviceProvider)
        {
            var (tracer, ctx, crm) = GetMainServices(serviceProvider);

            try
            {
                tracer.Trace($"Get {nameof(Entity)}");
                var target = ((Entity)ctx.InputParameters["Target"]).ToEntity<autodeal_agreement>();

                tracer.Trace($"Create {nameof(AgreementHandler)}");
                var service = new AgreementHandler(crm, tracer);
                service.EnsureDataConsistency(target);
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
