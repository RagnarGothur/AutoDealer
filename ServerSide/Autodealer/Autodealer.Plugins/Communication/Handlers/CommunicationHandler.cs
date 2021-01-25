using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System;
using System.Linq;

namespace Autodealer.Plugins.Communication.Handlers
{
    public class CommunicationHandler : BaseHandler
    {
        public CommunicationHandler(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        public void HandlePreCreate(autodeal_communication communication)
        {
            if (communication.autodeal_main.GetValueOrDefault(false))
            {
                CheckOnlyMain(communication);
            }
        }

        public void HandlePreUpdate(autodeal_communication communication)
        {
            if (communication.autodeal_main.GetValueOrDefault(false))
            {
                CheckOnlyMain(communication);
            }
        }

        private void CheckOnlyMain(autodeal_communication communication)
        {
            var communicationType = GetCommunicationType(communication);
            var query = new QueryExpression(autodeal_communication.EntityLogicalName);

            query.ColumnSet.AddColumns(
                autodeal_communication.Fields.autodeal_contactid, 
                autodeal_communication.Fields.autodeal_communicationId
            );

            var filter = new FilterExpression();
            query.Criteria.AddFilter(filter);

            filter.AddCondition(autodeal_communication.Fields.autodeal_main, ConditionOperator.Equal, true);
            filter.AddCondition(autodeal_communication.Fields.autodeal_type, ConditionOperator.Equal, (int)communicationType);
            filter.AddCondition(autodeal_communication.Fields.autodeal_communicationId, ConditionOperator.NotEqual, communication.Id);

            var contactLink = query.AddLink(Contact.EntityLogicalName, autodeal_communication.Fields.autodeal_contactid, Contact.PrimaryIdAttribute);
            contactLink.Columns.AddColumns(Contact.PrimaryIdAttribute);
            contactLink.LinkCriteria.AddCondition(Contact.PrimaryIdAttribute, ConditionOperator.Equal, communication.autodeal_contactid.Id);

            var otherCommunications = Crm.RetrieveMultiple(query).Entities.Select(c => c.ToEntity<autodeal_communication>());
            if (otherCommunications.Any())
            {
                throw new InvalidPluginExecutionException($"У выбранного контакта уже есть основное средство связи типа {communicationType}");
            }
        }

        private autodeal_communication_autodeal_type GetCommunicationType(autodeal_communication communication)
        {
            if (communication.autodeal_type.HasValue)
            {
                return (autodeal_communication_autodeal_type)communication.autodeal_type;
            }

            if (!String.IsNullOrEmpty(communication.autodeal_phone))
            {
                return autodeal_communication_autodeal_type.Telefon;
            }

            if (!String.IsNullOrEmpty(communication.autodeal_email))
            {
                return autodeal_communication_autodeal_type.Email;
            }

            throw new ArgumentException("invalid communication");
        }
    }
}
