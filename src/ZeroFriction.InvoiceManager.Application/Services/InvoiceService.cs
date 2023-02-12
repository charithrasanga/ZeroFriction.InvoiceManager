using ZeroFriction.InvoiceManager.Application.Mappers;
using ZeroFriction.InvoiceManager.Application.ViewModels;
using FluentMediator;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Domain.Invoice;

/*
 * Application service is that layer which initializes and oversees interaction 
 * between the domain objects and services. The flow is generally like this: 
 * get domain object (or objects) from repository, execute an action and put it 
 * (them) back there (or not). It can do more - for instance it can check whether 
 * a domain object exists or not and throw exceptions accordingly. So it lets the 
 * user interact with the application (and this is probably where its name originates 
 * from) - by manipulating domain objects and services. Application services should 
 * generally represent all possible use cases.
 */

namespace ZeroFriction.InvoiceManager.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceFactory _invoiceFactory;
        private readonly InvoiceViewModelMapper _invoiceViewModelMapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public InvoiceService(IInvoiceRepository invoiceRepository, InvoiceViewModelMapper invoiceViewModelMapper, ITracer tracer, IInvoiceFactory invoiceFactory, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceViewModelMapper = invoiceViewModelMapper;
            _tracer = tracer;
            _invoiceFactory = invoiceFactory;
            _mediator = mediator;
        }

        public async Task<InvoiceViewModel> Create(InvoiceViewModel invoiceViewModel)
        {
            using(var scope = _tracer.BuildSpan("Create_InvoiceService").StartActive(true))
            {
                var newInvoiceCommand = _invoiceViewModelMapper.ConvertToNewInvoiceCommand(invoiceViewModel);
                
                var invoiceResult = await _mediator.SendAsync<Invoice>(newInvoiceCommand);

                return _invoiceViewModelMapper.ConstructFromEntity(invoiceResult);
            }
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            using (var scope = _tracer.BuildSpan("Delete_InvoiceService").StartActive(true))
            {
                var deleteInvoiceCommand = _invoiceViewModelMapper.ConvertToDeleteInvoiceCommand(id);
                await _mediator.PublishAsync(deleteInvoiceCommand);
            }
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAll_InvoiceService").StartActive(true))
            {
                var invoicesEntities = await _invoiceRepository.FindAll();

                return _invoiceViewModelMapper.ConstructFromListOfEntities(invoicesEntities);
            }
        }

        public async Task<InvoiceViewModel> GetById(Guid id)
        {
            using (var scope = _tracer.BuildSpan("GetById_InvoiceService").StartActive(true))
            {
                var invoiceEntity = await _invoiceRepository.FindById(id);

                return _invoiceViewModelMapper.ConstructFromEntity(invoiceEntity);
            }
        }
    }
}
