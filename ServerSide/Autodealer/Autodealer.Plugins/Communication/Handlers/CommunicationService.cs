using Autodealer.Shared.Entities;
using Autodealer.Shared.Extensions;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System;
using System.Linq;

namespace Autodealer.Plugins.Communication.Handlers
{
    /// <summary>
    /// Сервис обслуживающий плагины сущности autodeal_communication
    /// </summary>
    public class CommunicationService : BaseService
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Клиент Crm api</param>
        /// <param name="tracer">Сервис трейсинга</param>
        public CommunicationService(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
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

        /// <summary>
        /// Проверяет отсутствие других основных средств коммуникации с заданным типом
        /// </summary>
        /// <param name="communication">Средство коммуникации</param>
        private void CheckOnlyMain(autodeal_communication communication)
        {
            var communicationType = GetCommunicationType(communication);
            var contactRef = GetContactRef(communication);

            Tracer.TraceCaller(
                $"checking that no more main {communicationType} communications of {contactRef.Id} exist"
            );

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
            contactLink.LinkCriteria.AddCondition(Contact.PrimaryIdAttribute, ConditionOperator.Equal, contactRef.Id);

            Tracer.TraceCaller("getting communications");
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

            Tracer.TraceCaller($"can't determine the type of the passed communication {communication.Id}; trying retrieve from db");

            return (autodeal_communication_autodeal_type)Crm.Retrieve(
                autodeal_communication.EntityLogicalName,
                communication.Id,
                new ColumnSet(autodeal_communication.Fields.autodeal_type)
            ).ToEntity<autodeal_communication>().autodeal_type;
        }

        private EntityReference GetContactRef(autodeal_communication communication)
        {
            if (communication.autodeal_contactid != null)
            {
                return communication.autodeal_contactid;
            }

            Tracer.TraceCaller($"can't determine related contact of the communication {communication.Id}; trying retrieve from db");

            var query = new QueryExpression(Contact.EntityLogicalName);

            query.ColumnSet.AddColumns(Contact.PrimaryIdAttribute);

            var communicationLink = query.AddLink(
                autodeal_communication.EntityLogicalName, 
                Contact.PrimaryIdAttribute, 
                autodeal_communication.Fields.autodeal_contactid
            );

            communicationLink.LinkCriteria.AddCondition(autodeal_communication.PrimaryIdAttribute, ConditionOperator.Equal, communication.Id);

            var contact = Crm.RetrieveMultiple(query).Entities.FirstOrDefault();
            if (contact is null)
            {
                throw new ArgumentException($"Can't determine related contact of the communication {communication.Id}");
            }

            return contact.ToEntityReference();
        }
    }
}
