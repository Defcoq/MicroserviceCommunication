using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BookCart.Services.Events;
using BookCart.Services.Handlers.Cart;
using BookShop.API.Extensions;
using Cart.Domain.Repositories;
using Cart.Domain.Services;
using Cart.Infrastructure.BackgroundServices;
using Cart.Infrastructure.Configurations;
using Cart.Infrastructure.Extensions;
using Cart.Infrastructure.Repositories;
using Cart.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cart.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services
                .AddScoped<ICartRepository, CartRepository>()
                .AddScoped<IBookShopService, BookShopService>()
                .AddBookShopService(new Uri(Configuration["BookShopApiUrl"]))
                .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
               // .AddIntegrationServices(Configuration)
               // .AddEventBus(Configuration)
                .AddEventBusCustom(Configuration)
                .AddHostedService<ItemSoldOutBackgroundService>()
                .Configure<CartDataSourceSettings>(Configuration);

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseRouting()
                .UseHttpsRedirection()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });

          //  ConfigureEventBus(app);
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BookDeleteIntegrationEvent, BookDeleteIntegrationEventHandler>();
            // eventBus.Subscribe<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
        }
    }
}