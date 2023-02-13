using FluentMediator;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using System.Reflection;
using ZeroFriction.InvoiceManager.API.Extensions;
using ZeroFriction.InvoiceManager.API.Extensions.Middleware;
using ZeroFriction.InvoiceManager.Application.Handlers;
using ZeroFriction.InvoiceManager.Application.Mappers;
using ZeroFriction.InvoiceManager.Application.Services;
using ZeroFriction.InvoiceManager.Domain.Invoices;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using ZeroFriction.InvoiceManager.Infrastructure.Persistence;
using ZeroFriction.InvoiceManager.Infrastructure.Repositories;

namespace ZeroFriction.InvoiceManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddSingleton<InvoiceViewModelMapper>();

            services.AddCosmosDb(Configuration);


            services.AddScoped<InvoiceCommandHandler>();
            services.AddScoped<InvoiceEventHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFluentMediator(builder =>
            {
                builder.On<CreateNewInvoiceCommand>().PipelineAsync().Return<Invoice, InvoiceCommandHandler>((handler, request) => handler.HandleNewInvoice(request));
                builder.On<InvoiceCreatedEvent>().PipelineAsync().Call<InvoiceEventHandler>((handler, request) => handler.HandleInvoiceCreatedEvent(request));

                builder.On<UpdateInvoiceCommand>().PipelineAsync().Return<Invoice, InvoiceCommandHandler>((handler, request) => handler.HandleUpdateInvoice(request));
                builder.On<InvoiceUpdatedEvent>().PipelineAsync().Call<InvoiceEventHandler>((handler, request) => handler.HandleInvoiceUpdatedEvent(request));

                builder.On<DeleteInvoiceCommand>().PipelineAsync().Call<InvoiceCommandHandler>((handler, request) => handler.HandleDeleteInvoice(request));
                builder.On<InvoiceDeletedEvent>().PipelineAsync().Call<InvoiceEventHandler>((handler, request) => handler.HandleInvoiceDeletedEvent(request));
            });

            services.AddSingleton(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            Log.Logger = new LoggerConfiguration().CreateLogger();

            services.AddOpenTracing();

            services.AddOptions();

            services.AddMvc();

            services.AddSwaggerGen();
        }

        // configure the HTTP request pipeline during runtime.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateAsyncScope();
                var context = scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
               
                // let's drop and re-create the database everytime
                context!.Database.EnsureDeleted();
                context!.Database.EnsureCreated();

            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZeroFriction Invoice Manager API V1");
            });
        }
    }
}
