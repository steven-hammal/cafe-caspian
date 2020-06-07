using CafeCaspian.Domain.Metadata;
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
    public class MenuItem
    {    
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public Temperature Temperature { get; private set; }
        public double Price { get; private set; }

        public MenuItem(string name, Category category, Temperature temperature, double price)
        {
            Name = name;
            Category = category;
            Temperature = temperature;
            Price = price;
        }
    }
}
