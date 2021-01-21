using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autodealer.ConsoleApp.Doers
{
    public class ContactDoer : BaseDoer
    {
        public ContactDoer(IOrganizationService crm) : base(crm)
        { }

        public override async Task DoAsync()
        {
            Log.Logger.Debug($"{nameof(ContactDoer)}: Constructing the query");
            QueryExpression query = GetQuery();

            EntityCollection retrieved = null; ;
            var moreRecords = true;
            var pageCount = 1;

            do
            {
                Log.Logger.Debug($"{nameof(ContactDoer)}: Retrieving multiple entities");

                retrieved = Crm.RetrieveMultiple(query);

                Log.Logger.Debug($"{nameof(ContactDoer)}: Got {retrieved.Entities.Count} entities; Page: {pageCount}");

                query.PageInfo = new PagingInfo() { PageNumber = pageCount++, PagingCookie = retrieved.PagingCookie };
                moreRecords = retrieved.MoreRecords;
            }
            while (moreRecords);

            var toCreate = new List<Entity>();

            foreach (var contactGroup in retrieved.Entities.Cast<Contact>().ToLookup(c => c.Id))
            {
                var contact = contactGroup.First();

                var containsPhoneCommunication = contactGroup.Any(c =>
                {
                    return c.Contains($"autodeal_communication1.{autodeal_communication.Fields.autodeal_phone}");
                });

                var containsEmailCommunication = contactGroup.Any(c =>
                {
                    return c.Contains($"autodeal_communication1.{autodeal_communication.Fields.autodeal_email}");
                });

                autodeal_communication comm;
                if (contact.Telephone1 != null && !containsPhoneCommunication)
                {
                    Log.Logger.Debug(
                        $"{nameof(ContactDoer)}: " +
                        $"create new {autodeal_communication_autodeal_type.Telefon} {nameof(autodeal_communication)}"
                    );

                    comm = GetNewCommunication(contact, autodeal_communication_autodeal_type.Telefon, true);
                    toCreate.Add(comm);
                }

                if (contact.EMailAddress1 != null && !containsEmailCommunication)
                {
                    Log.Logger.Debug(
                        $"{nameof(ContactDoer)}: " +
                        $"create new {autodeal_communication_autodeal_type.Email} {nameof(autodeal_communication)}"
                    );

                    comm = GetNewCommunication(contact, autodeal_communication_autodeal_type.Email, false);
                    toCreate.Add(comm);
                }
            }

            Log.Logger.Debug(
                $"{nameof(ContactDoer)}: " +
                $"sending entities to {SendEntityMode.Create}..."
            );

            await SendEntitiesAsync(SendEntityMode.Create, toCreate.ToArray());
        }

        private autodeal_communication GetNewCommunication(Contact contact, autodeal_communication_autodeal_type type, bool main)
        {
            var comm = new autodeal_communication(Guid.NewGuid());
            comm.autodeal_type = type;
            comm.autodeal_main = main;

            switch (type)
            {
                case autodeal_communication_autodeal_type.Telefon:
                    comm.autodeal_phone = contact.Telephone1;
                    break;
                case autodeal_communication_autodeal_type.Email:
                    comm.autodeal_email = contact.EMailAddress1;
                    break;
                default:
                    Log.Logger.Debug(
                        $"{nameof(ContactDoer)}: " +
                        $"unespected {nameof(autodeal_communication_autodeal_type)} {type}"
                    );

                    break;
            }

            comm.autodeal_name = $"{comm.autodeal_type} communication {comm.Id}";

            return comm;
        }

        private QueryExpression GetQuery()
        {
            var query = new QueryExpression(Contact.EntityLogicalName);

            query.ColumnSet.AddColumns(Contact.PrimaryIdAttribute, Contact.Fields.Telephone1, Contact.Fields.EMailAddress1);

            var query_Criteria_0 = new FilterExpression();
            query_Criteria_0.FilterOperator = LogicalOperator.Or;
            query_Criteria_0.AddCondition(Contact.Fields.EMailAddress1, ConditionOperator.NotNull);
            query_Criteria_0.AddCondition(Contact.Fields.Telephone1, ConditionOperator.NotNull);

            query.Criteria.AddFilter(query_Criteria_0);

            var query_autodeal_communication = query.AddLink(
                autodeal_communication.EntityLogicalName,
                Contact.PrimaryIdAttribute,
                autodeal_communication.Fields.autodeal_contactid,
                JoinOperator.LeftOuter
            );

            query_autodeal_communication.Columns.AddColumns(
                autodeal_communication.Fields.autodeal_communicationId,
                autodeal_communication.Fields.autodeal_email,
                autodeal_communication.Fields.autodeal_phone
            );

            return query;
        }
    }
}
