﻿namespace ConsoleHelpers
{
    public static class ConsoleApp
    {
        private const string _repeatMessage = "To run again, press 'y', to stop press any other key, to quit press 'Esc'";
        private const string _exitMessage = "Exiting program...";

        /// <summary>
        /// Method to get input from the user. This will loop and call <see cref="Console.ReadLine"/> 
        /// until the <paramref name="predicate"/> condition is met.
        /// </summary>
        /// <param name="predicate">The condition the user input must meet for the method to return.</param>
        /// <param name="failureMessage">The message to show to the user if the input doesn't match the <paramref name="predicate"/>.</param>
        /// <returns>The user input that matches the <paramref name="predicate"/>.</returns>
        public static string? GetInput(Func<string?, bool> predicate, string failureMessage)
        {
            var input = Console.ReadLine();
            while (!predicate(input))
            {
                Console.WriteLine(failureMessage);
                input = Console.ReadLine();
            }
            return input;
        }

        /// <summary>
        /// Method to get an input array from the user. This will loop and call <see cref="Console.ReadLine"/>
        /// until the <paramref name="predicate"/> condition is met, splitting the input based on 
        /// the <paramref name="separator"/> character.
        /// </summary>
        /// <param name="predicate">The condition the user input must meet for the method to return.</param>
        /// <param name="failureMessage">The message to show to the user if the input doesn't match the <paramref name="predicate"/>.</param>
        /// <param name="separator">The character to split the input based on. The default is space (' ').</param>
        /// <returns>The user input that matches the <paramref name="predicate"/>, split by the <paramref name="separator"/>.</returns>
        public static string[]? GetInputWithParams(Func<string[]?, bool> predicate, string failureMessage, char separator = ' ')
        {
            var input = Console.ReadLine();
            var inputArray = input?.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            while (!predicate(inputArray))
            {
                Console.WriteLine(failureMessage);
                input = Console.ReadLine();
                inputArray = input?.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            return inputArray;
        }

        /// <summary>
        /// Method to loop and perform the given <paramref name="action"/> until the user enters 
        /// anything except for 'Y' or 'y', showing the <paramref name="repeatMessage"/> message
        /// each time. The user can also enter 'Esc' to indicate they want to exit the program.
        /// </summary>
        /// <param name="action">The Action to perform every loop. Put your program code here.</param>
        /// <param name="repeatMessage">The message to show the user at the end of each loop, 
        /// to ask if they want to keep looping or exit.</param>
        /// <param name="exitMessage">The message to show the user when exiting the program.</param>
        /// <param name="endLoopMessage">The message to show the user when ending the loop 
        /// (not exiting the program). By default this is null and no message is shown.</param>
        /// <returns>True if the user pressed 'Esc', false if they pressed any key other than 'y'.</returns>
        public static bool LoopProgram(
            Action action,
            string repeatMessage = _repeatMessage,
            string exitMessage = _exitMessage,
            string? endLoopMessage = null)
        {
            while (true)
            {
                action();

                Console.WriteLine();
                Console.WriteLine(repeatMessage);
                

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine(exitMessage);
                    return true;
                }
                else if (char.ToLower(input.KeyChar) != 'y')
                {
                    endLoopMessage?.PrintToConsole();
                    return false;
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Method to loop and repeatedly call a given function until it returns false.
        /// </summary>
        /// <param name="action">The function to repeat that returns a bool.</param>
        /// <param name="exitMessage">The message to show when exiting the loop.</param>
        public static void LoopProgram(
            Func<bool> action,
            string exitMessage = _exitMessage)
        {
            while (!action()) { }
            Console.WriteLine();
            Console.WriteLine(exitMessage);
        }

        internal static string[]? GetOptionChoiceWithParams(
            int maxIndex,
            string failMsg = "Invalid entry, enter one of the numbers from the menu above.")
            => GetInputWithParams(s =>
                        s != null &&
                        int.TryParse(s.FirstOrDefault(), out int i)
                        && i >= 0 && i < maxIndex,
                    failMsg);
    }
}