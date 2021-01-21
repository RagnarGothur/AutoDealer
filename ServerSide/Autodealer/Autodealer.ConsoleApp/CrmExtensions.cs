using Microsoft.Xrm.Sdk;

using Serilog;

using System.Threading.Tasks;

namespace Autodealer.ConsoleApp
{
    public static class CrmExtensions
    {
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
