﻿using Autodealer.Plugins.Extensions;
using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using System;
using System.Linq;

namespace Autodealer.Plugins.Invoice.Handlers
{
    public class InvoiceHandler : BaseHandler
    {
        public InvoiceHandler(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        public void HandlePreCreate(autodeal_invoice invoice)
        {
            SetInvoiceType(invoice ?? throw new ArgumentNullException(nameof(invoice)));
        }

        public void HandlePostCreate(autodeal_invoice invoice)
        {
            EnsurePaidConsistency(invoice ?? throw new ArgumentNullException(nameof(invoice)));
        }

        public void HandlePostUpdate(autodeal_invoice invoice)
        {
            EnsurePaidConsistency(invoice ?? throw new ArgumentNullException(nameof(invoice)));
        }

        public void HandlePostDelete(EntityReference invoiceRef)
        {
            if (invoiceRef is null) throw new ArgumentNullException(nameof(invoiceRef));

            var columns = new ColumnSet(autodeal_invoice.Fields.autodeal_amount, autodeal_invoice.Fields.autodeal_fact);
            var toDelete = Crm.Retrieve(
                autodeal_invoice.EntityLogicalName,
                invoiceRef.Id,
                columns
            ).ToEntity<autodeal_invoice>();

            EnsurePaidConsistency(toDelete);
        }

        private void EnsurePaidConsistency(autodeal_invoice freshInvoice)
        {
            Tracer.TraceCaller("Getting invoices related to agreement of the fresh invoice");

            var invoicesQuery = new QueryExpression(autodeal_invoice.EntityLogicalName);
            invoicesQuery.ColumnSet.AddColumns(
                autodeal_invoice.PrimaryIdAttribute,
                autodeal_invoice.Fields.autodeal_fact, 
                autodeal_invoice.Fields.autodeal_amount, 
                autodeal_invoice.Fields.autodeal_dogovorid
            );

            var agreementLink = invoicesQuery.AddLink(
                autodeal_agreement.EntityLogicalName, 
                autodeal_invoice.Fields.autodeal_dogovorid, 
                autodeal_agreement.PrimaryIdAttribute
            );

            agreementLink.Columns.AddColumns(autodeal_agreement.PrimaryIdAttribute);
            agreementLink.LinkCriteria.AddCondition(
                autodeal_agreement.PrimaryIdAttribute, 
                ConditionOperator.Equal, 
                freshInvoice.autodeal_dogovorid.Id
            );

            Tracer.TraceCaller($"Retrieving them and casting to {nameof(autodeal_invoice)}");
            var otherInvoices = Crm.RetrieveMultiple(invoicesQuery).Entities.Select(i => i.ToEntity<autodeal_invoice>());

            Tracer.TraceCaller(
                $"Calculating the sum of the agreement {freshInvoice.autodeal_dogovorid.Id} ignoring a stored invoice {freshInvoice.Id} (current)"
            );

            decimal sum = otherInvoices
                .Where(i => i.autodeal_amount != null && i.autodeal_fact != null && (bool)i.autodeal_fact && i.Id != freshInvoice.Id)
                .Aggregate(new decimal(0), (accum, i) => accum += i.autodeal_amount.Value);

            Tracer.TraceCaller($"Getting the current invoice {freshInvoice.Id} from the retrieved invoices");
            var savedInvoice = otherInvoices.FirstOrDefault(i => i.Id != freshInvoice.Id);
            if (savedInvoice != null)
            {
                Tracer.TraceCaller($"The current invoice {freshInvoice.Id} has been got");
                freshInvoice.autodeal_amount = freshInvoice.autodeal_amount ?? savedInvoice.autodeal_amount;
                freshInvoice.autodeal_fact = freshInvoice.autodeal_fact ?? savedInvoice.autodeal_fact;
            }

            if (freshInvoice.autodeal_fact != null && (bool)freshInvoice.autodeal_fact)
            {
                Tracer.TraceCaller($"Adding the current invoice sum {freshInvoice.autodeal_amount.Value} to calculated sum {sum}");
                sum += freshInvoice.autodeal_amount.Value;
            }

            var agreement = new autodeal_agreement(freshInvoice.autodeal_dogovorid.Id)
            {
                autodeal_sum = new Money(sum)
            };

            Tracer.TraceCaller($"Updating agreement with calculated sum {sum}");
            Crm.Update(agreement);
        }

        private void SetInvoiceType(autodeal_invoice invoice)
        {
            Tracer.TraceCaller($"Check {nameof(Entity)} for {nameof(autodeal_invoice.Fields.autodeal_type)} filling");
            if (invoice.autodeal_type is null)
            {
                Tracer.TraceCaller($"Set {nameof(autodeal_invoice.autodeal_type)} to {autodeal_invoice_autodeal_type.Ruchnoe_sozdanie}");
                invoice[autodeal_invoice.Fields.autodeal_type] = new OptionSetValue((int)autodeal_invoice_autodeal_type.Ruchnoe_sozdanie);
            }
        }
    }
}
