using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuItem<T> : IMenuItem<T>
    {
        public string Name => throw new NotImplementedException();

        public string? Description => throw new NotImplementedException();

        public IList<IMenuItem>? SubItems => throw new NotImplementedException();

        public IMenuItem? MainMenu { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public T Display(int width)
        {
            throw new NotImplementedException();
        }

        public T Select(int width, string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
