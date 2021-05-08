using CafeCaspian.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeCaspian.Application
{
    public interface IOrderService
    {
        decimal GetTotalFor(IEnumerable<string> orderedItems);
    }
    public class OrderService : IOrderService
    {
        private readonly Menu _menu;
        private readonly ISurchargeService _surchargeService;

        public OrderService(Menu menu, ISurchargeService surchargeService)
        {
            _menu = menu;
            _surchargeService = surchargeService;
        }

        public decimal GetTotalFor(IEnumerable<string> orderedItems)
        {
            Validate(orderedItems);

            var orderedMenuItems = new List<MenuItem>();

            foreach (var item in orderedItems)
            {
                orderedMenuItems.Add(_menu.Items.Single(mi => mi.Name.Equals(item)));
            }

            var cheque = new Cheque(orderedMenuItems);
            var serviceCharge = _surchargeService.GetSurchargeFor(cheque);

            return decimal.Round(cheque.NetTotal + serviceCharge, 2, MidpointRounding.AwayFromZero); // My preferred way of rounding a decimal to a monetary value http://msdn.microsoft.com/en-us/library/9s0xa85y.aspx
        }

        private void Validate(IEnumerable<string> orderedItems)
        {
            var itemsNotOnMenu = orderedItems.Where(i => !_menu.Items.Any(mi => mi.Name.Equals(i)));

            if(itemsNotOnMenu.Any())
            {
                throw new ValidationException($"The following items are not on the menu: {string.Join(",", itemsNotOnMenu)}");
            }
        }
    }
}