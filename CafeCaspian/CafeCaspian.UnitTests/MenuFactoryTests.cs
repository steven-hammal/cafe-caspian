using CafeCaspian.Application;
using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using ExpectedObjects;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace CafeCaspian.UnitTests
{
    public class MenuFactoryTests
    {
        private readonly IMenuFactory _factory;

        public MenuFactoryTests()
        {
            _factory = new MenuFactory();
        }

        [Fact]
        public void It_builds_menu()
        {
            // Given
            var config = new ConfigurationBuilder()
                    .AddJsonFile("testAppsettings.json", false)
                    .Build();

            // When
            var result = _factory.GetMenuFromConfig(config.GetSection("TestMenu"));

            // Then
            var expectedMenu = new Menu();
            expectedMenu.AddMenuItems(new List<MenuItem> { 
                new MenuItem(name: "Test", category: Category.Drink, temperature: Temperature.Cold, price: 0.5m)
            });
            var expected = expectedMenu.ToExpectedObject();

            expected.ShouldMatch(result);
        }
    }
}
