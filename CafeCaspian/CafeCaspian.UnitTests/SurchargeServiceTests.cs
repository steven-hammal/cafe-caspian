using CafeCaspian.Application;
using CafeCaspian.Application.Config;
using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace CafeCaspian.UnitTests
{
    public class SurchargeServiceTests
    {
        private readonly Mock<IOptions<SurchargeOptions>> _mockOptions;
        private readonly ISurchargeService _service;

        public SurchargeServiceTests()
        {
            _mockOptions = new Mock<IOptions<SurchargeOptions>>();
            _mockOptions.SetupGet(o => o.Value).Returns(new SurchargeOptions 
            { 
                DefaultServiceRate = 1.0m,
                MaxServiceCharge = 20.0m,
                HotFoodServiceRate = 1.2m,
                FoodServiceRate = 1.1m
            });

            _service = new SurchargeService(_mockOptions.Object);
        }

        [Fact]
        public void It_applies_default_charge_when_no_food_is_ordered()
        {
            // Given
            var itemsWithNoFood = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "SomeDrink",
                    category: Category.Drink,
                    temperature: Temperature.Cold,
                    price: 1.0m
                )
            };
            var order = new Cheque(itemsWithNoFood);

            // When
            var result = _service.GetSurchargeFor(order);

            // Then
            result.ShouldBe(0.0m);
        }

        [Fact]
        public void It_applies_food_charge_when_cold_food_is_in_order()
        {
            // Given
            var itemsWithColdFood = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "SomeFood",
                    category: Category.Food,
                    temperature: Temperature.Cold,
                    price: 1.0m
                )
            };
            var order = new Cheque(itemsWithColdFood);

            // When
            var result = _service.GetSurchargeFor(order);

            // Then
            result.ShouldBe(0.1m);
        }

        [Fact]
        public void It_applies_hot_food_charge_when_hot_food_is_in_order()
        {
            // Given
            var itemsWithHotFood = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "SomeFood",
                    category: Category.Food,
                    temperature: Temperature.Hot,
                    price: 1.0m
                )
            };
            var order = new Cheque(itemsWithHotFood);

            // When
            var result = _service.GetSurchargeFor(order);

            // Then
            result.ShouldBe(0.2m);
        }

        [Fact]
        public void It_applies_service_charge_cap()
        {
            // Given
            var itemsWithExpensiveHotFood = new List<MenuItem>
            {
                new MenuItem
                (
                    name: "SomeFood",
                    category: Category.Food,
                    temperature: Temperature.Hot,
                    price: 250.0m
                )
            };
            var order = new Cheque(itemsWithExpensiveHotFood);

            // When
            var result = _service.GetSurchargeFor(order);

            // Then
            result.ShouldBe(20.0m);
        }
    }
}
