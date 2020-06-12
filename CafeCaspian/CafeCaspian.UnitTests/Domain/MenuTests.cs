using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using ExpectedObjects;
using System.Collections.Generic;
using Xunit;

namespace CafeCaspian.UnitTests.Domain
{
    public class MenuTests
    {
        [Fact]
        public void It_adds_items_to_menu()
        {
            // Given
            var menu = new Menu();
            var menuItems = new List<MenuItem>
            {
                new MenuItem(name: "TestItem", category: Category.Drink, temperature: Temperature.Cold, price: 0.10m)
            };

            // When
            menu.AddMenuItems(menuItems);

            // Then
            var expected = new List<MenuItem>
            {
                new MenuItem(name: "TestItem", category: Category.Drink, temperature: Temperature.Cold, price: 0.10m)
            }.ToExpectedObject();

            expected.ShouldMatch(menu.Items);

        }
    }
}
