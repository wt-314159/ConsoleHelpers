using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    internal static class StringBuilderExtensions
    {
        internal const string _tab = ".\t";

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

        internal static StringBuilder AppendLines(this StringBuilder builder, int numLines)
        {
            for (int i = 0; i < numLines; i++)
            {
                builder.AppendLine();
            }
            return builder;
        }

        internal static StringBuilder AppendSeparatorLine(
            this StringBuilder builder, 
            int width, 
            char separator = '-')
        {
            builder.Append(separator, width);
            return builder.AppendLine();
        }

        internal static StringBuilder AppendOption(
            this StringBuilder builder, 
            int index,
            string optionName,
            string tab = _tab)
        {
            builder.Append(index);
            builder.Append(tab);
            return builder.AppendLine(optionName);
        }

        internal static StringBuilder AppendOptions(
            this StringBuilder builder,
            IList<IMenuItem> options,
            string tab = _tab,
            string header = "Options:")
        {
            builder.AppendLine(header);
            builder.AppendLine();

            for (int i = 0; i < options.Count; i++)
            {
                builder.AppendOption(i, options[i].Name, tab);
            }
            return builder;
        }

        internal static StringBuilder AppendTitle(
            this StringBuilder builder,
            string title,
            int width,
            char separator = '-')
        {
            builder.AppendSeparatorLine(width, separator);
            builder.AppendLineCentred(title, width);
            return builder.AppendSeparatorLine(width, separator);
        }
    }
}
