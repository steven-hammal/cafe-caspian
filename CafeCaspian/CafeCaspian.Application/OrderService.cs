using CafeCaspian.Domain;
using System;
using System.Collections.Generic;

namespace CafeCaspian.Application
{
    public interface IOrderService
    {
        float GetTotalFor(IEnumerable<string> orderedItems); // TODO: Strongly type oredered items
    }
    public class OrderService : IOrderService
    {
        private readonly Menu _menu;

        public OrderService(Menu menu)
        {
            _menu = menu;
        }

        public float GetTotalFor(IEnumerable<string> orderedItems)
        {
            throw new NotImplementedException();
        }
    }
}
