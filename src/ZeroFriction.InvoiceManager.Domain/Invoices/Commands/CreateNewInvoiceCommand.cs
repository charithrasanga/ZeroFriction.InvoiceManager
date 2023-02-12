namespace ZeroFriction.InvoiceManager.Domain.Invoices.Commands
{
    public class CreateNewInvoiceCommand : InvoiceCommand
    {
        public CreateNewInvoiceCommand(string summary, string description)
        {
            Summary = summary;
            Description = description;
        }
    }
}
