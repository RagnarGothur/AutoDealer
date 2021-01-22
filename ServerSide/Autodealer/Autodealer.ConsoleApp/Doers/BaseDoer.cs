using Microsoft.Xrm.Sdk;

using Serilog;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autodealer.ConsoleApp.Doers
{
    /// <summary>
    /// Базовый класс исполнителя
    /// </summary>
    public abstract class BaseDoer : IDoer
    {
        /// <summary>
        /// Максимальное количество одновременных отправок сущностей
        /// </summary>
        public const byte MAX_SIMULTANEOUS_SENDINGS = 10;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(MAX_SIMULTANEOUS_SENDINGS);

        /// <summary>
        /// Crm api клиент
        /// </summary>
        protected IOrganizationService Crm { get; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Crm api клиент</param>
        public BaseDoer(IOrganizationService crm)
        {
            Crm = crm ?? throw new ArgumentNullException(nameof(crm));
        }

        /// <summary>
        /// Асинхронное выполнение задачи
        /// </summary>
        /// <returns></returns>
        public abstract Task DoAsync();

        /// <summary>
        /// Отправляет сущности на сервер
        /// </summary>
        /// <param name="mode">Режим отправки сущностей</param>
        /// <param name="entities">Сущности для отправки</param>
        /// <returns></returns>
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
