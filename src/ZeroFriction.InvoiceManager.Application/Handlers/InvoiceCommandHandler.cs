using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using FluentMediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Application.Mappers;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;

namespace ZeroFriction.InvoiceManager.Application.Handlers
{
    public class InvoiceCommandHandler
    {

        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;

        public InvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        public async Task<InvoiceHeader> HandleNewInvoice(CreateNewInvoiceCommand createNewInvoiceCommand)
        {

            var inv = new InvoiceHeader
            {
                Description = new Description(createNewInvoiceCommand.Description),
                InvoiceId = new InvoiceId(createNewInvoiceCommand.Id),
                Summary = new TotalAmount(createNewInvoiceCommand.Summary)
            };

            var invoiceCreated = await _invoiceRepository.Add(inv);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(
                new InvoiceCreatedEvent(invoiceCreated.InvoiceId.ToGuid(),
                invoiceCreated.Description.ToString(), 
                invoiceCreated.Summary.ToString())
                );

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
