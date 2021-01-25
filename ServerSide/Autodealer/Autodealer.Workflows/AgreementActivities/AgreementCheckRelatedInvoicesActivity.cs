using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

using System;
using System.Activities;
using System.Linq;

namespace Autodealer.Workflows.AgreementActivities
{
    public class AgreementCheckRelatedInvoicesActivity : CodeActivity
    {
        [Input("Agreement")]
        [RequiredArgument]
        [ReferenceTarget(autodeal_agreement.EntityLogicalName)]
        public InArgument<EntityReference> AgreementReference { get; set; }

        [Output("Are there any related invoices")]
        public OutArgument<bool> AreRelatedInvoices { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var tracer = context.GetExtension<ITracingService>();
            var crm = context.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(Guid.Empty);

            try
            {
                tracer.Trace("Getting agreement ref");
                var agreementRef = AgreementReference.Get(context);

                var service = new AgreementService(crm, tracer);
                var invoices = service.FindRelatedInvoices(agreementRef.Id);

                AreRelatedInvoices.Set(context, invoices.Any());
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
