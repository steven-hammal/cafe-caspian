using CafeCaspian.Domain.Metadata;

namespace CafeCaspian.Domain
{
    public class MenuItem
    {
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public Temperature Temperature { get; private set; }
        public decimal Price { get; private set; }

        public MenuItem(string name, Category category, Temperature temperature, decimal price)
        {
            Name = name;
            Category = category;
            Temperature = temperature;
            Price = price;
        }
    }
}
