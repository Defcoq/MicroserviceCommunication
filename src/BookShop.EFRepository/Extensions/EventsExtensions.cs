using BookShop.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.EFRepository.Extensions
{
    public static class EventsExtensions
    {
        public static IServiceCollection AddEventBusCustom(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new EventBusSettings();
            configuration.Bind("EventBus", config);
            services.AddSingleton(config);

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = config.HostName,
                UserName = config.User,
                Password = config.Password
            };

            services.AddSingleton(factory);

            return services;
        }
    }
}
