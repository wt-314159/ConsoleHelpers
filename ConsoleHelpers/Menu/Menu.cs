using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers.Menu
{
    public class Menu : MenuItem
    {
        public Menu(params IMenuItem[] menuItems) : this("Menu", null, menuItems)
        { }

        public Menu(string name, params IMenuItem[] menuItems) : this(name, null, menuItems)
        { }

        public Menu(string name, string? description, params IMenuItem[] menuItems)
            : base(name, description, menuItems)
        {
            ShowMainMenuOption = false;
        }
    }
}
