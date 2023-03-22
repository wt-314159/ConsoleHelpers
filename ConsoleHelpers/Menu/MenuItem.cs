using ConsoleHelpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuItem : IMenuItem
    {
        protected const string _tab = ".\t";
        protected const string _exitToMain = "Exit to Main Menu";
        protected static string? _separator;
        protected static int _separatorWidth;


        public string Name { get; }

        public string? Description { get; }

        public bool ShowMainMenuOption { get; }

        public Action? OnSelection { get; }

        public IList<IMenuItem>? SubItems { get; }


        public MenuItem(string name, Action action, string? description = null)
        {
            Name = name;
            OnSelection = action;
            SubItems = null;
            Description = description;
        }

        public MenuItem(string name, string? description = null, params IMenuItem[] subItems)
        {
            Name = name;
            SubItems = subItems;
            Description = description;
            OnSelection = null;
        }


        public virtual string Display(int width)
        {
            var builder = new StringBuilder();
            builder.AppendLine(GetSeparator(width));
            builder.AppendLineCentred(Name, width);
            builder.AppendLine(GetSeparator(width));

            if (SubItems != null && SubItems.Count > 0)
            {
                for (int i = 0; i < SubItems.Count; i++)
                {
                    builder.Append(i);
                    builder.Append(_tab);
                    builder.AppendLine(SubItems[i].Name);
                }
            }
            else if (Description != null)
            {
                builder.AppendLongStringLine(Description, width);
            }
            builder.AppendLine(GetSeparator(width));

            if (ShowMainMenuOption)
            {
                builder.Append(SubItems?.Count + 1);
                builder.Append(_tab);
                builder.Append(_exitToMain);
            }
            return builder.ToString();
        }



        protected static string GetSeparator(int width, char separatorChar = '=')
        {
            if (width != _separatorWidth || _separator == null)
            {
                var builder = new StringBuilder(width);
                builder.Append(separatorChar, width);
                _separator = builder.ToString();
                _separatorWidth = width;
            }
            return _separator;
        }
    }
}
