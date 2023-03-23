using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuSettings
    {
        public int Width { get; }
        public char Separator { get; }
        public bool ShowMainMenuOption { get; }

        public MenuSettings(int width, bool showMainMenuOption = true, char separator = '-')
        {
            Width = width;
            Separator = separator;
            ShowMainMenuOption = showMainMenuOption;
        }
    }
}
