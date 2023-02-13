using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            //    .RuleFor(m => m.Summary, f => new TotalAmount(f.Lorem.Text()))
            //    .RuleFor(m => m.Description, f => new Description(f.Lorem.Paragraph()));

            //// generate sample items
            //modelBuilder
            //    .Entity<Invoice>()
            //    .ToContainer("Invoices")
            //    .HasPartitionKey(x => x.InvoiceId)
            //    .HasData(invDataTemplate.GenerateBetween(2, 5));
        }

    }
}

