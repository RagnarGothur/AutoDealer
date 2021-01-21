using Microsoft.Xrm.Sdk;

using Serilog;

using System.Threading.Tasks;

namespace Autodealer.ConsoleApp
{
    /// <summary>
    /// Расширяет возможности клиента Crm api
    /// </summary>
    public static class CrmExtensions
    {
        /// <summary>
        /// Отправляет сущности на сервер
        /// </summary>
        /// <param name="crm"></param>
        /// <param name="mode">Режим отправки сущности</param>
        /// <param name="entity">Сущность для отправки</param>
        /// <returns></returns>
        public static Task SendAsync(this IOrganizationService crm, SendEntityMode mode, Entity entity)
        {
            Log.Logger.Debug($"Sending an entity: {nameof(mode)}: {mode}; {nameof(entity)}: {entity}, {entity.Id}...");

            switch (mode)
            {
                case SendEntityMode.Create:
                    var id = crm.Create(entity);

                    Log.Logger.Debug($"created {entity.GetType()}: {id}");
                    
                    break;
                case SendEntityMode.Update:
                    crm.Update(entity);
                    break;
                default:
                    //log
                    break;
            }

            Log.Logger.Debug($"Sent the entity: {nameof(mode)}: {mode}; {nameof(entity)}: {entity}, {entity.Id}");

            return Task.CompletedTask;
        }
    }
}
