using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public interface IMenuItem : IDisplayableMenuItem<IMenuItem>
    {
        void Display(MenuSettings settings, string[]? parameters);
        void Select(MenuSettings settings, string[]? parameters);
    }
}
