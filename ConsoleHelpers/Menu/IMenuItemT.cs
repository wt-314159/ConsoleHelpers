using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public interface IMenuItem<T> : IDisplayableMenuItem<IMenuItem<T>>
    {

        T Display(MenuSettings settings, string[]? parameters);
        T Select(MenuSettings settings, string[]? parameters);
    }
}
