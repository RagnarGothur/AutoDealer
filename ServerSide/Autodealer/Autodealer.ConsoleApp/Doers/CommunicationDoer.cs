using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Serilog;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autodealer.ConsoleApp.Doers
{
    /// <summary>
    /// Выполняет следующую задачу:
    /// Выбирает все объекты средства связи, где поле [основной]=Да, и устанавливает значения на связанном объекте Контакт значения
    /// Telephone1 – значение из найденного объекта Средство Связи, где основной=Да и Тип=Телефон
    /// emailaddress1 – значение из найденного объекта Средство Связи, где основной=Да и Тип= E-mail
    /// Приложение не должно обновлять запись Контакт, если необходимые данные уже записаны в его поля.
    /// </summary>
    public class CommunicationDoer : BaseDoer
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="crm">Crm api клиент</param>
        public CommunicationDoer(IOrganizationService crm) : base(crm)
        { }

        /// <summary>
        /// Выбирает все объекты средства связи, где поле [основной]=Да, и устанавливает значения на связанном объекте Контакт значения
        /// Telephone1 – значение из найденного объекта Средство Связи, где основной=Да и Тип=Телефон
        /// emailaddress1 – значение из найденного объекта Средство Связи, где основной=Да и Тип= E-mail
        /// Приложение не должно обновлять запись Контакт, если необходимые данные уже записаны в его поля.
        /// </summary>
        /// <returns></returns>
        public override async Task DoAsync()
        {
            Log.Logger.Debug($"{nameof(CommunicationDoer)}: Constructing the query");
            QueryExpression query = GetQuery();
            EntityCollection retrieved = null; ;
            var moreRecords = true;
            var pageCount = 1;

            do
            {
                Log.Logger.Debug($"{nameof(CommunicationDoer)}: Retrieving multiple entities");

                retrieved = Crm.RetrieveMultiple(query);

                Log.Logger.Debug($"{nameof(CommunicationDoer)}: Got {retrieved.Entities.Count} entities; Page: {pageCount}");

                query.PageInfo = new PagingInfo() { PageNumber = pageCount++, PagingCookie = retrieved.PagingCookie };
                moreRecords = retrieved.MoreRecords;
            }
            while (moreRecords);

            var toUpdate = new List<Entity>();

            foreach (autodeal_communication communication in retrieved.Entities.Cast<autodeal_communication>())
            {
                var contactToUpdate = new Contact(communication.autodeal_contactid.Id);

                switch (communication.autodeal_type)
                {
                    case autodeal_communication_autodeal_type.Telefon:
                        Log.Logger.Debug(
                            $"{nameof(CommunicationDoer)}: " +
                            $"{nameof(autodeal_communication)}: {communication.Id}; {autodeal_communication_autodeal_type.Telefon}"
                        );

                        var newPhone = communication.autodeal_phone;
                        var oldPhone = (string)communication.GetAttributeValue<AliasedValue>($"contact1.{Contact.Fields.Telephone1}").Value;

                        if (newPhone.Equals(oldPhone))
                        {
                            Log.Logger.Debug(
                                $"{nameof(CommunicationDoer)}: " +
                                $"shouldn't update {nameof(Contact)} {communication.autodeal_contactid.Id} because old and new phones are equal"
                            );

                            continue;
                        }

                        Log.Logger.Debug(
                            $"{nameof(CommunicationDoer)}: " +
                            $"update {nameof(Contact)} {communication.autodeal_contactid.Id} with new phone"
                        );

                        contactToUpdate[Contact.Fields.Telephone1] = newPhone;

                        break;
                    case autodeal_communication_autodeal_type.Email:
                        Log.Logger.Debug(
                            $"{nameof(CommunicationDoer)}: {nameof(autodeal_communication)}: {communication.Id}; {autodeal_communication_autodeal_type.Email}"
                        );

                        var newEmail = communication.autodeal_email;
                        var oldEmail = (string)communication.GetAttributeValue<AliasedValue>($"contact1.{Contact.Fields.EMailAddress1}").Value;

                        if (newEmail.Equals(oldEmail))
                        {
                            Log.Logger.Debug(
                                $"{nameof(CommunicationDoer)}: " +
                                $"shouldn't update {nameof(Contact)} {communication.autodeal_contactid.Id} because old and new emails are equal"
                            );

                            continue;
                        };

                        Log.Logger.Debug(
                            $"{nameof(CommunicationDoer)}: " +
                            $"update {nameof(Contact)} {communication.autodeal_contactid.Id} with new email"
                        );

                        contactToUpdate[Contact.Fields.EMailAddress1] = newEmail;

                        break;
                    default:
                        Log.Logger.Error(
                            $"{nameof(CommunicationDoer)}: " +
                            $"unexpected {nameof(communication.autodeal_type)}: {communication.autodeal_type}"
                        );

                        break;
                }

                toUpdate.Add(contactToUpdate);
            }

            await SendEntitiesAsync(SendEntityMode.Update, toUpdate.ToArray());
        }

        private QueryExpression GetQuery()
        {
            var query = new QueryExpression(autodeal_communication.EntityLogicalName);

            query.ColumnSet.AddColumns(
                autodeal_communication.Fields.autodeal_email,
                autodeal_communication.Fields.autodeal_phone,
                autodeal_communication.Fields.autodeal_type,
                autodeal_communication.Fields.autodeal_contactid
            );

            query.Criteria.AddCondition(autodeal_communication.Fields.autodeal_main, ConditionOperator.Equal, true);

            var query_contact = query.AddLink(
                Contact.EntityLogicalName,
                autodeal_communication.Fields.autodeal_contactid,
                Contact.Fields.Id
            );

            query_contact.Columns.AddColumns(Contact.Fields.Telephone1, Contact.Fields.EMailAddress1);

            return query;
        }
    }
}
