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

        public OrderService(Menu menu)
        {
            _menu = menu;
        }

        public decimal GetTotalFor(IEnumerable<string> orderedItems)
        {
            Validate(orderedItems);

            var cheque = new Cheque(_menu.Items.Where(mi => orderedItems.Contains(mi.Name)));

            return cheque.NetTotal;
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