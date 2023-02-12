using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using FluentMediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFriction.InvoiceManager.Application.Handlers
{
    public class InvoiceCommandHandler
    {
        private readonly IInvoiceFactory _invoiceFactory;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;

        public InvoiceCommandHandler(IInvoiceRepository invoiceRepository, IInvoiceFactory invoiceFactory, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceFactory = invoiceFactory;
            _mediator = mediator;
        }

        public async Task<Invoice> HandleNewInvoice(CreateNewInvoiceCommand createNewInvoiceCommand)
        {
            var invoice = _invoiceFactory.CreateInvoiceInstance(
                summary: new Domain.Invoices.ValueObjects.Summary(createNewInvoiceCommand.Summary),
                description: new Domain.Invoices.ValueObjects.Description(createNewInvoiceCommand.Description)
            );

            var invoiceCreated = await _invoiceRepository.Add(invoice);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new InvoiceCreatedEvent(invoiceCreated.InvoiceId.ToGuid(),
                invoiceCreated.Description.ToString(), invoiceCreated.Summary.ToString()));

            return invoiceCreated;
        }

        public async Task HandleDeleteInvoice(DeleteInvoiceCommand deleteInvoiceCommand)
        {
            await _invoiceRepository.Remove(deleteInvoiceCommand.Id);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new InvoiceDeletedEvent(deleteInvoiceCommand.Id));
        }
    }
}
