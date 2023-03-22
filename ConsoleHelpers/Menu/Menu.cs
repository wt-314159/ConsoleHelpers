using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class Menu : MenuItem
    {
        public Menu(params IMenuItem[] menuItems) : this("Menu", null, menuItems)
        { }

        public Menu(string title, params IMenuItem[] menuItems) : this(title, null, menuItems)
        { }

        public Menu(string title, string? description, params IMenuItem[] menuItems)
            : base(title, description, menuItems)
        {
            ShowMainMenuOption = false;
        }

        public void Show(int width = 0)
        {
            width = width == 0 ? Console.WindowWidth / 2 : width;
            Display(width);
        }
    }
}
