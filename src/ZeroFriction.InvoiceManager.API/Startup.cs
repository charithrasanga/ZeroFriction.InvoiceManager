using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.API.Extensions.Middleware;
using ZeroFriction.InvoiceManager.Application.Handlers;
using ZeroFriction.InvoiceManager.Application.Mappers;
using ZeroFriction.InvoiceManager.Application.Services;
using ZeroFriction.InvoiceManager.Domain.Invoice;
using ZeroFriction.InvoiceManager.Domain.Invoices.Commands;
using ZeroFriction.InvoiceManager.Domain.Invoices.Events;
using ZeroFriction.InvoiceManager.Infrastructure.Factories;
using ZeroFriction.InvoiceManager.Infrastructure.Repositories;
using FluentMediator;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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
            services.AddTransient<IInvoiceFactory, EntityFactory>();

            

            services.AddScoped<InvoiceCommandHandler>();
            services.AddScoped<InvoiceEventHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFluentMediator(builder =>
            {
                builder.On<CreateNewInvoiceCommand>().PipelineAsync().Return<Invoice, InvoiceCommandHandler>((handler, request) => handler.HandleNewInvoice(request));

                builder.On<InvoiceCreatedEvent>().PipelineAsync().Call<InvoiceEventHandler>((handler, request) => handler.HandleInvoiceCreatedEvent(request));

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
