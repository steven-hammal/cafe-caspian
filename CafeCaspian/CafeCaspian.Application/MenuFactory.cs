using CafeCaspian.Domain;
using CafeCaspian.Domain.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeCaspian.Application
{
    public interface IMenuFactory
    {
        public Menu GetMenuFromConfig(IConfigurationSection menuConfig);
    }
    public class MenuFactory : IMenuFactory
    {
        public Menu GetMenuFromConfig(IConfigurationSection menuConfig)
        {
            var menu = new Menu();

            var items = menuConfig.GetChildren()
                                  .ToList()
                                  .Select(x => new MenuItem
                                          (
                                            name: x.GetValue<string>("Name"),
                                            category: x.GetValue<Category>("Category"),
                                            temperature: x.GetValue<Temperature>("Temperature"),
                                            price: x.GetValue<double>("Price")
                                          )
                                  ).ToList();

            // TODO: Add error handling for malformed menuitmes in config

            menu.AddMenuItems(items);

            return menu;
        }
    }
}
