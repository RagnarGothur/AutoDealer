using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

using System;
using System.Activities;
using System.Linq;
using System.Threading.Tasks;

namespace Autodealer.Workflows.AgreementActivities
{
    public class AgreementPaymentScheduleActivity : CodeActivity
    {
        [Input("Agreement")]
        [RequiredArgument]
        [ReferenceTarget(autodeal_agreement.EntityLogicalName)]
        public InArgument<EntityReference> AgreementReference { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var tracer = context.GetExtension<ITracingService>();
            var crm = context.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(Guid.Empty);

            try
            {
                tracer.Trace("Getting agreement ref");
                var agreementRef = AgreementReference.Get(context);

                var service = new AgreementService(crm, tracer);
                var invoices = service.FindRelatedInvoices(
                    agreementRef.Id,
                    autodeal_invoice.PrimaryIdAttribute,
                    autodeal_invoice.Fields.autodeal_fact,
                    autodeal_invoice.Fields.autodeal_type
                );

                bool shouldReturn = invoices.Any(i =>
                    i.autodeal_fact.GetValueOrDefault(false)
                    ||
                    (i.autodeal_type == autodeal_invoice_autodeal_type.Ruchnoe_sozdanie)
                );

                if (shouldReturn) return;

                tracer.Trace("Retrieving agreement ref");
                var agreement = crm.Retrieve(
                    autodeal_agreement.EntityLogicalName,
                    agreementRef.Id,
                    new ColumnSet(
                        autodeal_agreement.Fields.autodeal_creditPeriod,
                        autodeal_agreement.Fields.autodeal_creditamount
                    )
                ).ToEntity<autodeal_agreement>();

                var tasks = new Task[]
                {
                    service.DeleteEntitiesAsync(
                        invoices.Where(i => i.autodeal_type == autodeal_invoice_autodeal_type.Avtomaticheskoe_sozdanie).ToArray()
                    ),
                    service.CreatePaymentScheduleAsync(agreement)
                };

                Task.WaitAll(tasks);
            }
            catch (Exception e)
            {
                tracer.Trace($"Exception catched: {e.GetType()} - {e.Message}");
                tracer.Trace(e.StackTrace);
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
