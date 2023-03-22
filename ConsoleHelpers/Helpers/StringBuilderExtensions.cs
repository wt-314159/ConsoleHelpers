using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers.Helpers
{
    internal static class StringBuilderExtensions
    {
        internal static StringBuilder AppendCentred(this StringBuilder builder, string value, int width)
        {
            var leftIndent = (width - value.Length) / 2;
            if (leftIndent > 0)
            {
                builder.Append(' ', leftIndent);
            }
            return builder.Append(value);
        }

        internal static StringBuilder AppendLineCentred(this StringBuilder builder, string value, int width)
        {
            builder.AppendCentred(value, width);
            return builder.AppendLine();
        }

        internal static StringBuilder AppendLongString(this StringBuilder builder, string value, int width)
        {
            if (value.Length <= width)
            {
                return builder.Append(value);
            }

            var span = value.AsSpan();
            var num = value.Length / width;
            var remainder = value.Length % width;

            for (int i = 0; i < num; i++)
            {
                var start = i * width;
                builder.Append(span.Slice(start, width));
                builder.AppendLine();
            }
            return builder.Append(span.Slice(num * width, remainder));
        }

        internal static StringBuilder AppendLongStringLine(this StringBuilder builder, string value, int width)
        {
            builder.AppendLongString(value, width);
            return builder.AppendLine();
        }
    }
}
