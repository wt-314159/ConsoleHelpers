using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public static class StringExtensions
    {
        public static void PrintToConsole(this string str)
            => Console.WriteLine(str);
    }
}
