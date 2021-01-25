using Autodealer.Shared;
using Autodealer.Shared.Entities;
using Autodealer.Shared.Extensions;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autodealer.Workflows.AgreementActivities
{
    public class AgreementService : EntityService
    {
        public AgreementService(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        public List<autodeal_invoice> FindRelatedInvoices(Guid agreementId, params string[] columns)
        {
            Tracer.TraceCaller("starting");
            var query = new QueryExpression(autodeal_invoice.EntityLogicalName);
            query.ColumnSet.AddColumns(columns);

            var agreementLink = query.AddLink(
                autodeal_agreement.EntityLogicalName,
                autodeal_invoice.Fields.autodeal_dogovorid,
                autodeal_agreement.PrimaryIdAttribute
            );

            agreementLink.LinkCriteria.AddCondition(autodeal_agreement.PrimaryIdAttribute, ConditionOperator.Equal, agreementId);

            Tracer.TraceCaller($"Retrieving {query.TopCount} related invoices to agreement {agreementId}");
            return Crm.RetrieveMultiple(query).Entities.Select(i => i.ToEntity<autodeal_invoice>()).ToList();
        }

        public async Task CreatePaymentScheduleAsync(autodeal_agreement agreement)
        {
            Tracer.TraceCaller("starting");

            if (agreement is null || agreement.autodeal_creditamount is null || agreement.autodeal_creditPeriod is null)
            {
                throw new ArgumentException("agreement, autodeal_creditamount, autodeal_creditPeriod must not be null");
            }

            var today = DateTime.Today;
            var paymentDate = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            var creditPeriodMonths = (int)agreement.autodeal_creditPeriod * 12;
            var sumPerInvoice = agreement.autodeal_creditamount.Value / creditPeriodMonths;

            Tracer.TraceCaller($"asyncronous start creating {creditPeriodMonths} credit schedule invoices");
            var tasks = new Task[creditPeriodMonths];
            for (int i = 0; i < creditPeriodMonths; i++)
            {
                var payDate = paymentDate; //По идее не нужно для значимых типов. На всякий случай
                tasks[i] = Task.Run(async () => await CreateScheduleInvoiceAsync(agreement, payDate, sumPerInvoice));
                paymentDate.AddMonths(1);
            }

            var toUpdate = new autodeal_agreement(agreement.Id)
            {
                autodeal_paymentplandate = today.AddDays(1)
            };

            Crm.Update(toUpdate);

            await Task.WhenAll(tasks);
        }

        private Task CreateScheduleInvoiceAsync(autodeal_agreement agreement, DateTime paymentDate, decimal paymentSum)
        {
            Tracer.TraceCaller($"starting with agreement = {agreement.Id}; paymentDate = {paymentDate}; paymentSum = {paymentSum}");

            var invoice = new autodeal_invoice(Guid.NewGuid())
            {
                autodeal_dogovorid = agreement.ToEntityReference(),
                autodeal_paydate = paymentDate,
                autodeal_amount = new Money(paymentSum),
                autodeal_type = autodeal_invoice_autodeal_type.Avtomaticheskoe_sozdanie
            };

            Crm.Create(invoice);

            return Task.CompletedTask;
        }
    }
}
