using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public class MenuItem<T> : MenuItemBase<IMenuItem<T>>, IMenuItem<T>
    {
        public Func<T>? OnSelection { get; }

        public Func<string[], T>? OnSelectionWithParams { get; }

        public MenuItem(string name, Func<T> action, string? description = null)
            : base(name, description, null)
        {
            OnSelection = action;
        }

        public MenuItem(string name, params IMenuItem<T>[] subItems)
            : this(name, null, subItems) { }

        public MenuItem(string name, string? description = null, params IMenuItem<T>[] subItems)
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

        public MenuItem(string name, Func<string[], T> action, string? description = null)
            : base(name, description, null)
        {
            OnSelectionWithParams = action;
        }

        public T Display(MenuSettings settings, string[]? parameters)
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
                    throw new Exception("Error parsing input"); // we don't know what T to return,
                                                                // so we have to throw error
                }
                else if (index == SubItems.Count && showMainMenu)
                {
                    if (MainMenu == null)
                    {
                        // Again, should never happen but should still handle it
                        Console.WriteLine("Unable to find main menu, exiting...");
                        throw new Exception("Error finding main menu");
                    }
                    return MainMenu.Display(settings, input);
                }
                else if (index >= 0 && index < SubItems.Count)
                {
                    return SubItems[index].Select(settings, input);
                }
                else
                {
                    // Again, shouldn't happen
                    // Maybe we should return a Result<T>, rather 
                    // than exceptions?
                    throw new Exception("Index out of range");
                }
            }
            else
            {
                // Shouldn't be displayed unless there are sub items,
                // however the display method is public so it could 
                // always be called by external code, so we have to
                // account for every possibility.
                // TODO allow user to select main menu as an option,
                // for now just select current item
                return Select(settings, Array.Empty<string>());
            }
        }

        public T Select(MenuSettings settings, string[]? parameters)
        {
            if (parameters?.Length > 0 && (parameters[0] == "help" || parameters[0] == "-help"))
            {
                Display(settings, parameters);
                Console.WriteLine();
                Console.WriteLine();
                var input = Console.ReadLine();
                return Select(settings, input?.Split(' '));
            }
            if (OnSelectionWithParams != null)
            {
                return OnSelectionWithParams(parameters ?? Array.Empty<string>());
            }
            else if (OnSelection != null)
            {
                return OnSelection();
            }
            else
            {
                return Display(settings, parameters);
            }
        }
    }
}
