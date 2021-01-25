using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins
{
    public abstract class BaseHandler
    {
        protected IOrganizationService Crm { get; }
        protected ITracingService Tracer { get; }

        public BaseHandler(IOrganizationService crm, ITracingService tracer)
        {
            Crm = crm ?? throw new ArgumentNullException(nameof(crm));
            Tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            Tracer.Trace($"{this} created");
        }
    }
}
