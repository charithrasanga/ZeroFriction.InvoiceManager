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
using System.Linq;

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

        public async Task<Invoice> HandleNewInvoice(CreateNewInvoiceCommand createNewInvoiceCommand)
        {

            double totalAmount = createNewInvoiceCommand.InvoiceLines.Sum(x=>x.LineTotal.ToDouble());
            var inv = new Invoice
            {
                Description = new Description(createNewInvoiceCommand.Description),
                InvoiceId = new InvoiceId(createNewInvoiceCommand.Id),
                TotalAmount = new TotalAmount(totalAmount),
                InvoiceDate = createNewInvoiceCommand.InvoiceDate,
                InvoiceLines = createNewInvoiceCommand.InvoiceLines,
              
            };

            var invoiceCreated = await _invoiceRepository.Add(inv);

            // raise an event in case you need to notify  this change to other subscriber
            await _mediator.PublishAsync(
                new InvoiceCreatedEvent(invoiceCreated.InvoiceId.ToGuid(),
                invoiceCreated.Description.ToString())
                );

            return invoiceCreated;
        }

        public async Task<Invoice> HandleUpdateInvoice(UpdateInvoiceCommand updateInvoiceCommand)
        {
            var foundInvoice = await _invoiceRepository.FindById(updateInvoiceCommand.Id);


            foundInvoice.InvoiceDate = updateInvoiceCommand.InvoiceDate;
            foundInvoice.Description = new Description(updateInvoiceCommand.Description);
            foundInvoice.InvoiceLines = updateInvoiceCommand.InvoiceLines;
            double totalAmount = updateInvoiceCommand.InvoiceLines.Sum(x => x.LineTotal.ToDouble());
            foundInvoice.TotalAmount = new TotalAmount(totalAmount);

          
            var invoiceUpdated  = await _invoiceRepository.Update(foundInvoice);

            // raise an event in case you need to notify  this change to other subscriber
            await _mediator.PublishAsync(
                new InvoiceUpdatedEvent(invoiceUpdated.InvoiceId.ToGuid(),
                invoiceUpdated.Description.ToString())
                );

            return invoiceUpdated;
        }

        public async Task HandleDeleteInvoice(DeleteInvoiceCommand deleteInvoiceCommand)
        {
            await _invoiceRepository.Remove(deleteInvoiceCommand.Id);

            // raise an event in case you need to notify  this change to other subscriber
            await _mediator.PublishAsync(new InvoiceDeletedEvent(deleteInvoiceCommand.Id));
        }
    }
}
