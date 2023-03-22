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
        protected const string _exitToMain = "Back to Main Menu";
        protected static string? _separator;
        protected static int _separatorWidth;

        public IMenuItem? MainMenu { get; set; }

        public string Name { get; }

        public string? Description { get; }

        public bool ShowMainMenuOption { get; set; }

        public Action? OnSelection { get; }

        public Action<string[]>? OnSelectionWithParams { get; }

        public IList<IMenuItem>? SubItems { get; }


        public MenuItem(string name, Action action, string? description = null)
        {
            Name = name;
            OnSelection = action;
            SubItems = null;
            Description = description;
            ShowMainMenuOption = true;
        }

        public MenuItem(string name, params IMenuItem[] subItems)
            : this(name, null, subItems) { }

        public MenuItem(string name, string? description = null, params IMenuItem[] subItems)
        {
            Name = name;
            SubItems = subItems;
            Description = description;
            OnSelection = null;
            ShowMainMenuOption = true;
            if (MainMenu != null)
            {
                foreach (var item in subItems)
                {
                    item.MainMenu = this.MainMenu;
                }
            }
        }

        public MenuItem(string name, Action<string[]> action, string? description = null)
        {
            Name = name;
            OnSelectionWithParams = action;
            SubItems = null;
            Description = description;
            ShowMainMenuOption = true;
        }

        public virtual void Select(int width, string[] parameters)
        {
            if (parameters?.Length > 0 && (parameters[0] == "help" || parameters[0] == "-help"))
            {
                Display(width);
                Console.WriteLine();
                Console.WriteLine();
                var input = Console.ReadLine();
                Select(width, input?.Split(' ') ?? Array.Empty<string>());
            }
            if (OnSelectionWithParams != null)
            {
                OnSelectionWithParams(parameters ?? Array.Empty<string>());
            }
            else if (OnSelection != null)
            {
                OnSelection();
            }
            else
            {
                Display(width);
            }
        }


        public virtual void Display(int width)
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine();

            builder.AppendLine(GetSeparator(width));
            builder.AppendLineCentred(Name, width);
            builder.AppendLine(GetSeparator(width));

            if (SubItems != null && SubItems.Count > 0)
            {
                builder.AppendLine("Option:");
                builder.AppendLine();
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
                builder.Append(SubItems?.Count);
                builder.Append(_tab);
                builder.AppendLine(_exitToMain);
                builder.AppendLine(GetSeparator(width));
            }
            Console.WriteLine(builder.ToString());

            if (SubItems != null && SubItems.Count > 0)
            {
                var maxIndex = ShowMainMenuOption ? SubItems.Count + 1 : SubItems.Count;
                var input = ConsoleApp.GetInputWithParams(s =>
                        s != null &&
                        int.TryParse(s.FirstOrDefault(), out int i)
                        && i >= 0 && i < maxIndex,
                    "Invalid entry, enter one of the numbers from the menu above.");
                if (int.TryParse(input?.FirstOrDefault(), out int index))
                {
                    if (index == SubItems.Count)
                    {
                        MainMenu?.Display(width);
                    }
                    else if (index >= 0 && index < SubItems.Count)
                    {
                        SubItems[index].Select(width, input.SkipFirst());
                    }
                }
            }
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
