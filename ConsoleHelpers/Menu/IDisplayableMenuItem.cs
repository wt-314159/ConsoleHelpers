using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public interface IDisplayableMenuItem<T> where T : IDisplayableMenuItem<T>
    {
        string Name { get; }
        string? Description { get; }
        public IList<T>? SubItems { get; }
        public T? MainMenu { get; set; }
    }
}
