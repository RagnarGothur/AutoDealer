using Autodealer.Shared.Entities;

using Microsoft.Xrm.Sdk;

namespace Autodealer.Plugins.Invoice.Handlers
{
    public class InvoiceService : BaseService
    {
        public InvoiceService(IOrganizationService crm, ITracingService tracer) : base(crm, tracer)
        { }

        public Entity SetInvoiceType(Entity invoice)
        {
            invoice.Attributes.TryGetValue(autodeal_invoice.Fields.autodeal_type, out object type);

            Tracer.Trace($"Check {nameof(Entity)} for {nameof(autodeal_invoice.Fields.autodeal_type)} filling");
            if (type is null)
            {
                Tracer.Trace($"Set {nameof(autodeal_invoice.autodeal_type)} to {autodeal_invoice_autodeal_type.Ruchnoe_sozdanie}");
                invoice[autodeal_invoice.Fields.autodeal_type] = new OptionSetValue((int)autodeal_invoice_autodeal_type.Ruchnoe_sozdanie);
            }

            return invoice;
        }
    }
}
