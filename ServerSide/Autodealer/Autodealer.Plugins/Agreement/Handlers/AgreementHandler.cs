using Autodealer.Plugins.Extensions;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System;
using System.Linq;

namespace Autodealer.Plugins.Agreement.Handlers
{
    public class AgreementHandler : BaseHandler
    {
        public AgreementHandler(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        public void EnsureDataConsistency(autodeal_agreement agreement)
        {
            EnsureContactDateConsistency(agreement);
        }

        private void EnsureContactDateConsistency(autodeal_agreement agreement)
        {
            Tracer.TraceCaller($"{nameof(EnsureContactDateConsistency)}: create query");
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
