using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public interface IMenuItem<T>
    {
        string Name { get; }
        string? Description { get; }
        public IList<IMenuItem>? SubItems { get; }
        public IMenuItem? MainMenu { get; set; }

        T Display(int width);
        T Select(int width, string[] parameters);
    }
}
