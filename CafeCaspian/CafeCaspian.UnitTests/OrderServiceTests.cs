using CafeCaspian.Application;
using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using ExpectedObjects;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace CafeCaspian.UnitTests
{
    public class OrderServiceTests
    {
        private readonly IOrderService _service;
        private readonly Menu _menu;

        public OrderServiceTests()
        {
            var testMenuItems = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "ItemOne",
                    category: Category.Food,
                    temperature: Temperature.Cold,
                    price: 2.00
                ),
                 new MenuItem
                (
                    name: "ItemTwo",
                    category: Category.Drink,
                    temperature: Temperature.Cold,
                    price: 3.00
                )
            };

            _menu = new Menu();
            _menu.AddMenuItems(testMenuItems);

            _service = new OrderService(_menu);
        }

        [Fact]
        public void It_throws_if_ordered_item_is_not_on_menu()
        {
            // Given
            var orderedItems = new List<string>() { "ItemX" };

            // When
            var exception = Should.Throw<ValidationException>(() => _service.GetTotalFor(orderedItems));

            // Then
            exception.Message.ShouldBe("ItemX is not on the menu");
        }

        [Fact]
        public void It_gets_total_for_order()
        {
            // Given
            var orderedItems = new List<string>() { "ItemOne", "ItemTwo" };

            // When
            var result = _service.GetTotalFor(orderedItems);

            // Then
            result.ShouldBe(5.00f);
        }
    }
}
