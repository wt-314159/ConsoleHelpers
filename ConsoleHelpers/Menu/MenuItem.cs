using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuItem : MenuItemBase<IMenuItem>, IMenuItem
    {
        public Action? OnSelection { get; }

        public Action<string[]>? OnSelectionWithParams { get; }


        public MenuItem(string name, Action action, string? description = null)
            : base(name, description, null)
        {
            OnSelection = action;
        }

        public MenuItem(string name, params IMenuItem[] subItems)
            : this(name, null, subItems) { }

        public MenuItem(string name, string? description = null, params IMenuItem[] subItems)
            : base(name, description, subItems)
        {
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
            : base(name, description, null)
        {
            OnSelectionWithParams = action;
        }

        public virtual void Select(MenuSettings settings, string[]? parameters)
        {
            if (parameters?.Length > 0 && (parameters[0] == "help" || parameters[0] == "-help"))
            {
                Display(settings, parameters);
                Console.WriteLine();
                Console.WriteLine();
                var input = Console.ReadLine();
                Select(settings, input?.Split(' '));
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
                Display(settings, parameters);
            }
        }


        public virtual void Display(MenuSettings settings, string[]? parameters)
        {
            var showMainMenu = settings.ShowMainMenuOption && MainMenu != null;
            ShowMenuOptions(settings);


            if (SubItems != null && SubItems.Count > 0)
            {
                int index = GetUserSelection(showMainMenu, out string[]? input);
                if (index == -1)
                {
                    // Should never happen, but we should still handle gracefully
                    Console.WriteLine("Error parsing input, exiting...");
                }
                if (index == SubItems.Count && showMainMenu)
                {
                    MainMenu?.Display(settings, input);
                }
                else if (index >= 0 && index < SubItems.Count)
                {
                    SubItems[index].Select(settings, input);
                }
            }
            else
            {
                // Shouldn't be displayed unless there are sub items,
                // however the display method is public so it could 
                // always be called by external code, so we have to
                // account for every possibility.
                // In this case, just show 2 options, select the action
                // or escape to main menu (if there show main menu = true)
                Select(settings, Array.Empty<string>());    
            }
        }
    }
}
