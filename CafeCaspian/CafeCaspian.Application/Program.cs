using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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

            RegisterServices(config);

            var menuFactory = new MenuFactory();
            var menu = menuFactory.GetMenuFromConfig(config.GetSection("Menu"));

            var purchasedItems = args[0].Split(",");

            Console.WriteLine(purchasedItems[0]);
            Console.ReadLine();

            DisposeServices();
        }

        private static void RegisterServices(IConfiguration config)
        {
            var services = new ServiceCollection();
            services.AddOptions();

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
