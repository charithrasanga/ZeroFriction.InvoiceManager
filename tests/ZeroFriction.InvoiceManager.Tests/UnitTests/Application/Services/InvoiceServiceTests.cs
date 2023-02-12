using ZeroFriction.InvoiceManager.Application.Mappers;
using ZeroFriction.InvoiceManager.Application.Services;
using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using ZeroFriction.InvoiceManager.Tests.UnitTests.Helpers;
using FluentMediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Moq;
using OpenTracing;
using OpenTracing.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZeroFriction.InvoiceManager.Tests.UnitTests.Application.Services
{
    public class InvoiceServiceTests
    {
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepository = new Mock<IInvoiceRepository>();
        private readonly Mock<IInvoiceFactory> _mockInvoiceFactory = new Mock<IInvoiceFactory>();
        private readonly Mock<ITracer> _mockITracer = new Mock<ITracer>();
        private readonly Mock<IMediator> _mockIMediator = new Mock<IMediator>();
        private static readonly Mock<IHttpContextAccessor> _mockIHttpContextAccessor = new Mock<IHttpContextAccessor>();

        private readonly InvoiceViewModelMapper _mockInvoiceViewModelMapper = new InvoiceViewModelMapper(_mockIHttpContextAccessor.Object);

        [Fact]
        public async System.Threading.Tasks.Task Create_Success()
        {
            //Arrange
            _mockITracer.Setup(x => x.BuildSpan(It.IsAny<string>())).Returns(() => new MockSpanBuilder(new MockTracer(), ""));
            _mockIMediator.Setup(x => x.SendAsync<Invoice>(It.IsAny<CreateNewInvoiceCommand>(), null))
                .Returns(System.Threading.Tasks.Task.FromResult(InvoiceHelper.GetInvoice()));
            _mockIHttpContextAccessor.Setup(x => x.HttpContext).Returns(HttpContextHelper.GetHttpContext());

            //Act
            var invoiceService = new InvoiceService(_mockInvoiceRepository.Object, _mockInvoiceViewModelMapper, _mockITracer.Object, _mockInvoiceFactory.Object, _mockIMediator.Object);
            var result = await invoiceService.Create(InvoiceViewModelHelper.GetInvoiceViewModel());

            //Assert
            Assert.NotNull(result);

            Assert.Equal("Summary", result.Summary);
            Assert.Equal("Description", result.Description);

            Assert.NotNull(result.Id);
            Assert.NotNull(result.Description);

            _mockITracer.Verify(x => x.BuildSpan(It.IsAny<string>()), Times.Once);
            _mockIMediator.Verify(x => x.SendAsync<Invoice>(It.IsAny<CreateNewInvoiceCommand>(), null), Times.Once);
        }
    }
}
