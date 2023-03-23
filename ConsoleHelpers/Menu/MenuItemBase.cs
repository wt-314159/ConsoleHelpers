using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelpers
{
    public abstract class MenuItemBase<T> : IDisplayableMenuItem<T> where T : IDisplayableMenuItem<T>
    {
        protected const string _exitToMain = "Back to Main Menu";

        public string Name { get; }

        public string? Description { get; }

        public virtual IList<T>? SubItems { get; }

        public virtual T? MainMenu { get; set; }

        public MenuItemBase(string name, string? description = null, IList<T>? subItems = null)
        {
            Name = name;
            Description = description;
            SubItems = subItems;
        }

        public MenuItemBase(string name, string? description = null, params T[]? subItems)
        {
            Name = name;
            Description = description;
            SubItems = subItems;
        }

        protected virtual void ShowMenuOptions(MenuSettings settings)
        {
            var width = settings.Width;
            var showMainMenu = settings.ShowMainMenuOption && MainMenu != null;

            var builder = new StringBuilder();
            builder.AppendLines(3);
            builder.AppendTitle(Name, width);

            if (SubItems != null && SubItems.Count > 0)
            {
                builder.AppendOptions(SubItems.Select(x => x.Name).ToList());
            }
            else if (Description != null)
            {
                builder.AppendLongStringLine(Description, width);
            }
            builder.AppendSeparatorLine(width);

            ShowMainMenuOptionIfTrue(showMainMenu, builder, width);
            Console.WriteLine(builder.ToString());
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

        protected virtual int GetUserSelection(
            bool showMainMenu,
            out string[]? parameters)
        {
            parameters = null;
            if (SubItems == null) { return -1; }
            var maxIndex = showMainMenu ? SubItems.Count + 1 : SubItems.Count;

            var input = ConsoleApp.GetOptionChoiceWithParams(maxIndex);

            if (int.TryParse(input?.FirstOrDefault(), out int index))
            {
                parameters = input?.SkipFirst();
                return index;
            }
            else return -1;
        }
    }
}
