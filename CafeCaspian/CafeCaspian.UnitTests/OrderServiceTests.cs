using CafeCaspian.Application;
using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using FluentValidation;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace CafeCaspian.UnitTests
{
    public class OrderServiceTests
    {
        private readonly IOrderService _service;
        private readonly Menu _menu;
        private readonly Mock<ISurchargeService> _mockSurchargeService;

        public OrderServiceTests()
        {
            var testMenuItems = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "ItemOne",
                    category: Category.Food,
                    temperature: Temperature.Cold,
                    price: 2.00m
                ),
                 new MenuItem
                (
                    name: "ItemTwo",
                    category: Category.Drink,
                    temperature: Temperature.Cold,
                    price: 3.00m
                )
            };

            _menu = new Menu();
            _menu.AddMenuItems(testMenuItems);

            _mockSurchargeService = new Mock<ISurchargeService>();
            _mockSurchargeService.Setup(m => m.GetSurchargeFor(It.IsAny<Cheque>())).Returns(1.0m);

            _service = new OrderService(_menu, _mockSurchargeService.Object);
        }

        [Fact]
        public void It_throws_if_ordered_item_is_not_on_menu()
        {
            // Given
            var orderedItems = new List<string>() { "ItemX" };

            // When
            var exception = Should.Throw<ValidationException>(() => _service.GetTotalFor(orderedItems));

            // Then
            exception.Message.ShouldBe("The following items are not on the menu: ItemX");
        }

        [Fact]
        public void It_gets_total_for_order()
        {
            // Given
            var orderedItems = new List<string>() { "ItemOne", "ItemTwo" };

            // When
            var result = _service.GetTotalFor(orderedItems);

            // Then
            result.ShouldBe(6.00m);
        }
    }
}
