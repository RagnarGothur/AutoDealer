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
    /// <summary>
    /// Выполняет следующую задачу:
    /// Выбирает объекты Контакт, для которых заполнено поле telephone1  или emailaddress1 и нет связанных объектов Контактная информация с указанными значениями. Для каждого найденного Контакта, система создает новый объект в соответствии с правилами: +++
    /// Если указан только telephone1, то создается объект средство связи с заполненным номером телефона и типом Тип=Телефон, значением в поле основной = Да и названием =”Телефон”
    /// Если указан только emailaddress1, то создается объект средство связи с заполненным адресом электронной почты и типом Тип = Email, значением в поле основной=Нет и названием=”Email”
    /// Если указаны оба значения, система :
    /// создает объект средство связи с заполненным номером телефона и типом Тип=Телефон, значением в поле основной = Да и названием =”Телефон”
    /// создает объект средство связи с заполненным адресом электронной почты и типом Тип = Email, значением в поле основной=Нет и названием=”Email”
    /// <returns></returns>
    /// </summary>
    public class ContactDoer : BaseDoer
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Crm api клиент</param>
        public ContactDoer(IOrganizationService crm) : base(crm)
        { }

        /// <summary>
        /// Выбирает объекты Контакт, для которых заполнено поле telephone1  или emailaddress1 и нет связанных объектов Контактная информация с указанными значениями. Для каждого найденного Контакта, система создает новый объект в соответствии с правилами: +++
        /// Если указан только telephone1, то создается объект средство связи с заполненным номером телефона и типом Тип=Телефон, значением в поле основной = Да и названием =”Телефон”
        /// Если указан только emailaddress1, то создается объект средство связи с заполненным адресом электронной почты и типом Тип = Email, значением в поле основной=Нет и названием=”Email”
        /// Если указаны оба значения, система :
        /// создает объект средство связи с заполненным номером телефона и типом Тип=Телефон, значением в поле основной = Да и названием =”Телефон”
        /// создает объект средство связи с заполненным адресом электронной почты и типом Тип = Email, значением в поле основной=Нет и названием=”Email”
        /// </summary>
        /// <returns></returns>
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
            // Instantiate QueryExpression query
            var query = new QueryExpression("contact");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("telephone1", "emailaddress1");

            // Define filter query.Criteria
            var query_Criteria_0 = new FilterExpression();
            query.Criteria.AddFilter(query_Criteria_0);

            // Define filter query_Criteria_0
            query_Criteria_0.FilterOperator = LogicalOperator.Or;
            query_Criteria_0.AddCondition("telephone1", ConditionOperator.NotNull);
            query_Criteria_0.AddCondition("emailaddress1", ConditionOperator.NotNull);

            // Add link-entity query_autodeal_communication
            var query_autodeal_communication = query.AddLink("autodeal_communication", "contactid", "autodeal_contactid", JoinOperator.LeftOuter);

            // Add columns to query_autodeal_communication.Columns
            query_autodeal_communication.Columns.AddColumns("autodeal_communicationid", "autodeal_email", "autodeal_phone");


            return query;
        }
    }
}
