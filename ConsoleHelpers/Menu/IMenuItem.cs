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
        public IList<IMenuItem>? SubItems { get; }
        public IMenuItem? MainMenu { get; set;  }
        bool ShowMainMenuOption { get; }

        void Display(int width);
        void Select(int width, string[] parameters);
    }
}
