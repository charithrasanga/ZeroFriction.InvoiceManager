using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.ValueObjects;
using ZeroFriction.InvoiceManager.Infrastructure.Converters;

namespace ZeroFriction.InvoiceManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
            .Properties<InvoiceId>()
            .HaveConversion<InvoiceIdConverter>();

            configurationBuilder
            .Properties<TotalAmount>()
            .HaveConversion<TotalAmountConverter>();

            configurationBuilder
            .Properties<Description>()
            .HaveConversion<DescriptionConverter>();

            configurationBuilder
           .Properties<ItemCode>()
           .HaveConversion<ItemCodeConverter>();

            configurationBuilder
           .Properties<LineTotal>()
           .HaveConversion<LineTotalConverter>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //var invDataTemplate = new Faker<Invoice>()
            //    .RuleFor(m => m.InvoiceId, f => new InvoiceId(f.Random.Guid()))
            //    .RuleFor(m => m.InvoiceDate, f => f.Date.Past())
            //    .RuleFor(m => m.TotalAmount, f => new TotalAmount(Convert.ToDouble(f.Finance.Amount(150, 1000))))
            //     .RuleFor(m => m.InvoiceLines, f => f.Make(f.Random.Number(2,10), () => new InvoiceLine { 

            //         ItemCode = new ItemCode( f.Random.Replace("*********")),
            //         LineTotal = new LineTotal(Convert.ToDouble(f.Finance.Amount(60, 250))),
            //         Qty =f.Random.Int(1,5),
            //         UnitPrice= Convert.ToDouble(f.Finance.Amount(10, 50)),

            //     }))
            //    .RuleFor(m => m.Description, f => new Description(f.Lorem.Paragraph()));

            modelBuilder
                .Entity<Invoice>()
                .ToContainer("Invoices")
                .HasPartitionKey(x => x.InvoiceId);

            //modelBuilder
            //    .Entity<Invoice>()
            //    .ToContainer("Invoices")
            //    .HasPartitionKey(x => x.InvoiceId)
            //    .HasData(invDataTemplate.GenerateBetween(5, 10));
        }

    }
}

