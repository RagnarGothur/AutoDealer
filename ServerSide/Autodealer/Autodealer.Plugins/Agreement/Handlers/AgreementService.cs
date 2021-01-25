using Autodealer.Shared.Entities;
using Autodealer.Shared.Extensions;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System.Linq;

namespace Autodealer.Plugins.Agreement.Handlers
{
    /// <summary>
    /// Сервис обслуживающий плагины сущности autodeal_agreement
    /// </summary>
    public class AgreementService : BaseService
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Клиент Crm api</param>
        /// <param name="tracer">Сервис трейсинга</param>
        public AgreementService(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        /// <summary>
        /// Обеспечивает консистентность данных после создания/изменения сущности
        /// </summary>
        /// <param name="agreement">Договор</param>
        public void EnsureDataConsistency(autodeal_agreement agreement)
        {
            EnsureContactDateConsistency(agreement);
        }

        /// <summary>
        /// Автоматически заполняет поле [Дата первого договора] на объекте Контакт. 
        /// Поле заполняется датой договора из объекта Договор, при условии, что создаваемый договор – первый.
        /// </summary>
        /// <param name="agreement">Договор</param>
        private void EnsureContactDateConsistency(autodeal_agreement agreement)
        {
            var query = new QueryExpression(autodeal_agreement.EntityLogicalName);
            query.ColumnSet.AddColumns(autodeal_agreement.PrimaryIdAttribute, autodeal_agreement.Fields.autodeal_contact);
            query.Criteria.AddCondition(autodeal_agreement.PrimaryIdAttribute, ConditionOperator.NotEqual, agreement.Id);

            var contactLink = query.AddLink(Contact.EntityLogicalName, autodeal_agreement.Fields.autodeal_contact, Contact.PrimaryIdAttribute);
            contactLink.Columns.AddColumns(Contact.PrimaryIdAttribute);
            contactLink.LinkCriteria.AddCondition(Contact.PrimaryIdAttribute, ConditionOperator.Equal, agreement.autodeal_contact.Id);

            Tracer.TraceCaller($"trying retrieve multiple {nameof(autodeal_agreement)}");
            EntityCollection retrieved = Crm.RetrieveMultiple(query);

            if (!retrieved.Entities.Any())
            {
                Tracer.TraceCaller($"no more {nameof(autodeal_agreement)} rel to {nameof(Contact)} {agreement.autodeal_contact.Id}");
                Tracer.TraceCaller($"set {nameof(Contact)} {agreement.autodeal_contact.Id} date to {agreement.autodeal_date}");

                var contact = new Contact(agreement.autodeal_contact.Id);
                contact.autodeal_date = agreement.autodeal_date;
                Crm.Update(contact);
            }
        }
    }
}
