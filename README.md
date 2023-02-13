## Read Me

Welcome to **ZeroFriction.InvoiceManager**

### Prerequisites
- .Net 6 needs to be installed
- Visual Studio 2022 or later version / VS Code
- Azure Cosmos DB Emulator or Azure CosmosDB Instance

### Setting up the source code 
- Clone this repo
- Open the solution from  IDE of your choice
- Restore Nuget Packages
- Make sure your Cosmos DB Emulator is Up and running ( only if youare using emulator)
- obtain the Primary Connection String for your Cosmos DB Instance
- Change the connection in **appsettings-dev.json** inside `   ZeroFriction.InvoiceManager\src\ZeroFriction.InvoiceManager.API\Config\`
( depending on your environment you can maintain seperate connection strings)
- Set  `ZeroFriction.InvoiceManager.API` as startup project and run
- you can use the Swagger UI to intract with the API

### Project Structure


```
📦 
├─ .gitattributes
├─ .gitignore
├─ LICENSE
├─ README.md
├─ ZeroFriction.InvoiceManager.sln
├─ src
│  ├─ Dockerfile
│  ├─ ZeroFriction.InvoiceManager.API
│  │  ├─ Config
│  │  │  ├─ appsettings-dev.json
│  │  │  ├─ appsettings-prod.json
│  │  │  └─ appsettings-stag.json
│  │  ├─ Controllers
│  │  │  └─ InvoiceController.cs
│  │  ├─ Extensions
│  │  │  ├─ Middleware
│  │  │  │  ├─ ErrorDetails.cs
│  │  │  │  └─ ExceptionMiddleware.cs
│  │  │  └─ ServiceExtensions.cs
│  │  ├─ Program.cs
│  │  ├─ Properties
│  │  │  └─ launchSettings.json
│  │  ├─ Startup.cs
│  │  └─ ZeroFriction.InvoiceManager.API.csproj
│  ├─ ZeroFriction.InvoiceManager.Application
│  │  ├─ Handlers
│  │  │  ├─ InvoiceCommandHandler.cs
│  │  │  └─ InvoiceEventHandler.cs
│  │  ├─ Mappers
│  │  │  └─ InvoiceViewModelMapper.cs
│  │  ├─ Services
│  │  │  ├─ IInvoiceService.cs
│  │  │  └─ InvoiceService.cs
│  │  ├─ ViewModels
│  │  │  └─ InvoiceViewModel.cs
│  │  └─ ZeroFriction.InvoiceManager.Application.csproj
│  ├─ ZeroFriction.InvoiceManager.Domain
│  │  ├─ IAggregateRoot.cs
│  │  ├─ IRepository.cs
│  │  ├─ Invoices
│  │  │  ├─ Commands
│  │  │  │  ├─ CreateNewInvoiceCommand.cs
│  │  │  │  ├─ DeleteInvoiceCommand.cs
│  │  │  │  ├─ InvoiceCommand.cs
│  │  │  │  └─ UpdateInvoiceCommand.cs
│  │  │  ├─ Events
│  │  │  │  ├─ InvoiceCreatedEvent.cs
│  │  │  │  ├─ InvoiceDeletedEvent.cs
│  │  │  │  ├─ InvoiceEvent.cs
│  │  │  │  └─ InvoiceUpdatedEvent.cs
│  │  │  ├─ IInvoiceRepository.cs
│  │  │  ├─ Invoice.cs
│  │  │  ├─ InvoiceLine.cs
│  │  │  └─ ValueObjects
│  │  │     ├─ Description.cs
│  │  │     ├─ InvoiceId.cs
│  │  │     ├─ ItemCode.cs
│  │  │     ├─ LineTotal.cs
│  │  │     └─ TotalAmount.cs
│  │  └─ ZeroFriction.InvoiceManager.Domain.csproj
│  └─ ZeroFriction.InvoiceManager.Infrastructure
│     ├─ Converters
│     │  ├─ DescriptionConverter.cs
│     │  ├─ InvoiceIdConverter.cs
│     │  ├─ ItemCodeConverter.cs
│     │  ├─ LineTotalConverter.cs
│     │  └─ TotalAmountConverter.cs
│     ├─ Persistence
│     │  └─ ApplicationDbContext.cs
│     ├─ Repositories
│     │  └─ InvoiceRepository.cs
│     └─ ZeroFriction.InvoiceManager.Infrastructure.csproj
└─ tests
   └─ ZeroFriction.InvoiceManager.Tests
      ├─ UnitTests
      │  ├─ Application
      │  │  └─ Services
      │  │     └─ InvoiceServiceTests.cs
      │  └─ Helpers
      │     ├─ HttpContextHelper.cs
      │     ├─ InvoiceHelper.cs
      │     └─ InvoiceViewModelHelper.cs
      └─ ZeroFriction.InvoiceManager.Tests.csproj
```

### Solution structure
- **Application layer ** impliments the business logic
- **Domain layer** is where our domain  will be modeled and  patterns like Repository Pattern (only interfaces, not implementation) will be defined
- **Infrastructure layer**  implements data access implementation where there need to be a seperation from Application layer