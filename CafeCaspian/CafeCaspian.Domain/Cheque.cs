using CafeCaspian.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeCaspian.Domain
{
    public class Cheque
    {
        // Leaving this mutable as is likely that functionality around adding/removing items from orders could be needed
        public IEnumerable<MenuItem> OrderedItems { get; set; }
        public decimal NetTotal { get; private set; }
        public decimal GrossTotal { get; private set; }
        private readonly decimal HotFoodServiceRate = 1.2m;
        private readonly decimal FoodServiceRate = 1.1m;
        private readonly decimal DefaultServiceRate = 1.0m;
        private readonly decimal MaxServiceCharge = 20.0m;

        public Cheque(IEnumerable<MenuItem> orderedItems)
        {
            OrderedItems = orderedItems;
            NetTotal = OrderedItems.Sum(i => i.Price);
            GrossTotal = GetGrossTotal(); // Gonna have to take this into a method if you want cheques to be mutable
        }

        private decimal GetServiceRate()
        { 
            if(ContainsHotFood())
            {
                return HotFoodServiceRate;
            }

            if(ContainsFood())
            {
                return FoodServiceRate;
            }

            return DefaultServiceRate;
        }

        private decimal GetGrossTotal()
        {
            var serviceRate = GetServiceRate();

            var grossTotal = NetTotal * serviceRate;

            if(grossTotal - NetTotal > MaxServiceCharge)
            {
                grossTotal = NetTotal + MaxServiceCharge;
            }

            return grossTotal;
        }
        private bool ContainsFood()
        {
            return OrderedItems.Any(i => i.Category == Category.Food);
        }

        private bool ContainsHotFood()
        {
            return OrderedItems.Any(i => i.Category == Category.Food && i.Temperature == Temperature.Hot);
        }
    }
}
