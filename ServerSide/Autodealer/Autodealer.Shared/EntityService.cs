using Microsoft.Xrm.Sdk;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autodealer.Shared
{
    /// <summary>
    /// Базовый сервис
    /// </summary>
    public abstract class EntityService
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
        public EntityService(IOrganizationService crm, ITracingService tracer)
        {
            Crm = crm ?? throw new ArgumentNullException(nameof(crm));
            Tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            Tracer.Trace($"{this.GetType().Name} created");
        }

        public async Task DeleteEntitiesAsync(params Entity[] entities)
        {
            var tasks = new List<Task>();
            foreach (Entity entity in entities)
            {
                var e = entity; // для правильного замыкания
                tasks.Add(Task.Run(() => 
                    Crm.Delete(e.LogicalName, e.Id))
                );
            }

            await Task.WhenAll(tasks);
        }
    }
}
