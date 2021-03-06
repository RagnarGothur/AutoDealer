﻿using Autodealer.Plugins.Invoice.Handlers;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins.Invoice.Plugins
{
    public class PostInvoiceCreate : BasePlugin, IPlugin
    {
        public override void Execute(IServiceProvider serviceProvider)
        {
            var (tracer, ctx, crm) = GetMainServices(serviceProvider);

            try
            {
                tracer.Trace($"Get {nameof(Entity)}");
                var target = ((Entity)ctx.InputParameters["Target"]).ToEntity<autodeal_invoice>();

                tracer.Trace($"Create {nameof(InvoiceService)}");
                var service = new InvoiceService(crm, tracer);

                service.HandlePostCreate(target);
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
