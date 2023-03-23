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
            foreach (var item in menuItems)
            {
                item.MainMenu = this;
            }
        }

        public void Show(MenuSettings settings)
        {
            Display(settings, Array.Empty<string>());
        }

        protected override void ShowMainMenuOptionIfTrue(bool _, StringBuilder __, MenuSettings ___)
        {
            return;
        }
    }
}
