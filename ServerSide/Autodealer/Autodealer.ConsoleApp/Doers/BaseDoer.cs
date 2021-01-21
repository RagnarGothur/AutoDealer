using Microsoft.Xrm.Sdk;

using Serilog;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autodealer.ConsoleApp.Doers
{
    public abstract class BaseDoer : IDoer
    {
        public const byte MAX_SIMULTANEOUS_SENDINGS = 10;

        private SemaphoreSlim _semaphore = new SemaphoreSlim(MAX_SIMULTANEOUS_SENDINGS);

        protected IOrganizationService Crm { get; }

        public BaseDoer(IOrganizationService crm)
        {
            Crm = crm ?? throw new ArgumentNullException(nameof(crm));
        }

        public abstract Task DoAsync();

        protected async Task SendEntitiesAsync(SendEntityMode mode, params Entity[] entities)
        {
            Log.Logger.Debug($"Sending {entities.Length} entities: {nameof(mode)}: {mode} in {MAX_SIMULTANEOUS_SENDINGS} tasks");
            var tasks = new List<Task>(entities.Length);

            foreach (Entity entity in entities)
            {
                Log.Logger.Debug($"Current semaphore state: {_semaphore.CurrentCount}");
                await _semaphore.WaitAsync();

                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        Log.Logger.Debug($"Sending an entity: {nameof(mode)}: {mode}; {nameof(entity)}: {entity}, {entity.Id}...");
                        await Crm.SendAsync(mode, entity);
                    }
                    finally
                    {
                        Log.Logger.Debug($"Release semaphore");
                        _semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
