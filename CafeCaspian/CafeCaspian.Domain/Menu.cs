using System.Collections.Generic;

namespace CafeCaspian.Domain
{
    public class Menu
    {
        private readonly List<MenuItem> _items;
        public IReadOnlyCollection<MenuItem> Items => _items.AsReadOnly();

        public Menu()
        {
            _items = new List<MenuItem>();
        }

        public void AddMenuItems(IEnumerable<MenuItem> items)
        {
            _items.AddRange(items);
        }
    }
    
}
