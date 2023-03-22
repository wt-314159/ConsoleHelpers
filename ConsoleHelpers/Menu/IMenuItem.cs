using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public interface IMenuItem
    {
        string Name { get; }
        string? Description { get; }
        Action? OnSelection { get; }
        public IList<IMenuItem>? SubItems { get; }
        bool ShowMainMenuOption { get; }

        string Display(int width);

    }
}
