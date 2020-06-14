using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CafeCaspian.UnitTests.Domain
{
    public class ChequeTests
    {
        private readonly Menu _menu;

        public ChequeTests()
        {
            _menu = new Menu();
            var menuItems = new List<MenuItem>
            {
                new MenuItem(name: "ColdDrink", category: Category.Drink, temperature: Temperature.Cold, price: 0.10m),
                new MenuItem(name: "ColdFood", category: Category.Food, temperature: Temperature.Cold, price: 0.10m),
                new MenuItem(name: "HotFood", category: Category.Food, temperature: Temperature.Hot, price: 0.10m)
            };
            _menu.AddMenuItems(menuItems);
        }

        [Fact]
        public void It_calculates_net_total()
        {
            //Given
            var orderedMenuItems = _menu.Items;

            // When
            var cheque = new Cheque(orderedMenuItems);

            // Then
            cheque.NetTotal.ShouldBe(0.3m);
        }

        [Fact]
        public void It_returns_true_if_order_contains_food()
        {
            //Given
            var orderedMenuItems = _menu.Items.Where(i => i.Category == Category.Food);
            var cheque = new Cheque(orderedMenuItems);

            // When
            var result = cheque.ContainsFood();

            // Then
            result.ShouldBeTrue();
        }

        [Fact]
        public void It_returns_false_if_order_does_not_contain_food()
        {
            //Given
            var orderedMenuItems = _menu.Items.Where(i => i.Category == Category.Drink);
            var cheque = new Cheque(orderedMenuItems);

            // When
            var result = cheque.ContainsFood();

            // Then
            result.ShouldBeFalse();
        }

        [Fact]
        public void It_returns_true_if_order_contains_hot_food()
        {
            //Given
            var orderedMenuItems = _menu.Items.Where(i => i.Category == Category.Food &&
                                                        i.Temperature == Temperature.Hot);
            var cheque = new Cheque(orderedMenuItems);

            // When
            var result = cheque.ContainsHotFood();

            // Then
            result.ShouldBeTrue();
        }

        [Fact]
        public void It_returns_false_if_order_contains_no_hot_food()
        {
            //Given
            var orderedMenuItems = _menu.Items.Where(i => i.Category == Category.Food &&
                                                        i.Temperature == Temperature.Cold);
            var cheque = new Cheque(orderedMenuItems);

            // When
            var result = cheque.ContainsHotFood();

            // Then
            result.ShouldBeFalse();
        }
    }
}
