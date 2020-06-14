using CafeCaspian.Domain.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace CafeCaspian.Domain
{
    public class Cheque
    {
        public IEnumerable<MenuItem> OrderedItems { get; set; }
        public decimal NetTotal { get; private set; }

        public Cheque(IEnumerable<MenuItem> orderedItems)
        {
            OrderedItems = orderedItems;
            NetTotal = OrderedItems.Sum(i => i.Price);
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
