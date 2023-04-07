using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class Menu<T> : MenuItem<T>
    {
        public Menu(params IMenuItem<T>[] menuItems) : this("Menu", null, menuItems)
        { }

        public Menu(string title, params IMenuItem<T>[] menuItems) : this(title, null, menuItems)
        { }

        public Menu(string title, string? description, params IMenuItem<T>[] menuItems)
            : base(title, description, menuItems)
        {
            foreach (var item in menuItems)
            {
                item.MainMenu = this;
            }
        }

        public T Show(MenuSettings settings)
        {
            return Display(settings, Array.Empty<string>());
        }

        protected override void ShowMainMenuOptionIfTrue(bool _, StringBuilder __, MenuSettings ___)
        {
            return;
        }
    }
}
