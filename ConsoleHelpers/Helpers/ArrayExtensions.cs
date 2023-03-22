using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers.Helpers
{
    internal static class ArrayExtensions
    {
        internal static T[] SkipFirst<T>(this T[]? input)
        {
            if (input == null)
            {
                return new T[0];
            }
            else if (input.Length < 2)
            {
                return input;
            }
            else
            {
                var output = new T[input.Length - 1];
                for (int i = 1; i < input.Length; i++)
                {
                    output[i - 1] = input[i];
                }
                return output;
            }

        }
    }
}
