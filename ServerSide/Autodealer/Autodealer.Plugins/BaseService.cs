using Microsoft.Xrm.Sdk;

using System;

namespace Autodealer.Plugins
{
    /// <summary>
    /// Базовый сервис
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Клиент Crm api
        /// </summary>
        protected IOrganizationService Crm { get; }

        /// <summary>
        /// Сервис трейсинга
        /// </summary>
        protected ITracingService Tracer { get; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Клиент Crm api</param>
        /// <param name="tracer">Сервис трейсинга</param>
        public BaseService(IOrganizationService crm, ITracingService tracer)
        {
            Crm = crm ?? throw new ArgumentNullException(nameof(crm));
            Tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            Tracer.Trace($"{this} created");
        }
    }
}
