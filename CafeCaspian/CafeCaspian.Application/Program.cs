﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using FluentValidation;
using CafeCaspian.Application.Config;

namespace CafeCaspian.Application
{
    class Program
    {
        public static IConfiguration config;
        public static IServiceProvider serviceProvider; 

        static void Main(string[] args)
        {
            config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", false)
              .Build();

            RegisterServices();

            var menuFactory = new MenuFactory();
            var menu = menuFactory.GetMenuFromConfig(config.GetSection("Menu"));
            var orderService = new OrderService(menu, serviceProvider.GetRequiredService<ISurchargeService>());

            var orderedItems = args;

            try
            {
                var total = orderService.GetTotalFor(orderedItems);
                Console.WriteLine(total);
            }
            catch(ValidationException exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadLine();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.Configure<SurchargeOptions>(config.GetSection(SurchargeOptions.Surcharge));
            services.AddSingleton<ISurchargeService, SurchargeService>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable) serviceProvider).Dispose();
            }
        }
    }    
}
