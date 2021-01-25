using Autodealer.Plugins.Communication.Handlers;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins.Communication.Plugins
{
    public class PreCommunicationCreate : BasePlugin, IPlugin
    {
        public override void Execute(IServiceProvider serviceProvider)
        {
            var (tracer, ctx, crm) = GetMainServices(serviceProvider);

            try
            {
                tracer.Trace($"Get {nameof(Entity)}");
                var target = ((Entity)ctx.InputParameters["Target"]).ToEntity<autodeal_communication>();

                tracer.Trace($"Create {nameof(CommunicationHandler)}");
                var service = new CommunicationHandler(crm, tracer);

                service.HandlePreCreate(target);
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
