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

        public Cheque(IEnumerable<MenuItem> orderedItems)
        {
            OrderedItems = orderedItems;
        }

        public bool ContainsFood()
        {
            return OrderedItems.Any(i => i.Category == Category.Food);
        }

        public bool ContainsHotFood()
        {
            return OrderedItems.Any(i => i.Category == Category.Food && i.Temperature == Temperature.Hot);
        }
    }
}
