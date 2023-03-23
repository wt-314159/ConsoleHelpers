using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuItem : IMenuItem
    {
        protected const string _exitToMain = "Back to Main Menu";

        public IMenuItem? MainMenu { get; set; }

        public string Name { get; }

        public string? Description { get; }

        public Action? OnSelection { get; }

        public Action<string[]>? OnSelectionWithParams { get; }

        public IList<IMenuItem>? SubItems { get; }


        public MenuItem(string name, Action action, string? description = null)
        {
            Name = name;
            OnSelection = action;
            SubItems = null;
            Description = description;
        }

        public MenuItem(string name, params IMenuItem[] subItems)
            : this(name, null, subItems) { }

        public MenuItem(string name, string? description = null, params IMenuItem[] subItems)
        {
            Name = name;
            SubItems = subItems;
            Description = description;
            OnSelection = null;
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
        }

        public virtual void Select(MenuSettings settings, string[] parameters)
        {
            if (parameters?.Length > 0 && (parameters[0] == "help" || parameters[0] == "-help"))
            {
                Display(settings, parameters ?? Array.Empty<string>());
                Console.WriteLine();
                Console.WriteLine();
                var input = Console.ReadLine();
                Select(settings, input?.Split(' ') ?? Array.Empty<string>());
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
                Display(settings, parameters ?? Array.Empty<string>());
            }
        }


        public virtual void Display(MenuSettings settings, string[] parameters)
        {
            var width = settings.Width;
            var showMainMenu = settings.ShowMainMenuOption && MainMenu != null;

            var builder = new StringBuilder();
            builder.AppendLines(3);
            builder.AppendTitle(Name, width);

            if (SubItems != null && SubItems.Count > 0)
            {
                builder.AppendOptions(SubItems);
            }
            else if (Description != null)
            {
                builder.AppendLongStringLine(Description, width);
            }
            builder.AppendSeparatorLine(width);

            ShowMainMenuOptionIfTrue(showMainMenu, builder, width);
            Console.WriteLine(builder.ToString());

            if (SubItems != null && SubItems.Count > 0)
            {
                var maxIndex = showMainMenu ? SubItems.Count + 1 : SubItems.Count;

                var input = ConsoleApp.GetOptionChoiceWithParams(maxIndex);

                if (int.TryParse(input?.FirstOrDefault(), out int index))
                {
                    if (index == SubItems.Count && showMainMenu)
                    {
                        MainMenu?.Display(settings, input.SkipFirst());
                    }
                    else if (index >= 0 && index < SubItems.Count)
                    {
                        SubItems[index].Select(settings, input.SkipFirst());
                    }
                }
            }
        }

        protected virtual void ShowMainMenuOptionIfTrue(
            bool showMainMenu, 
            StringBuilder builder,
            int width)
        {
            if (showMainMenu)
            {
                builder.AppendOption(SubItems?.Count ?? 0, _exitToMain);
                builder.AppendSeparatorLine(width);
            }
        }
    }
}
