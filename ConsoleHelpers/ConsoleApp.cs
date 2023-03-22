using ConsoleHelpers.Menu;

namespace ConsoleHelpers
{
    public static class ConsoleApp
    {
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
        public static string[]? GetInput(Func<string[]?, bool> predicate, string failureMessage, char separator = ' ')
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
        /// each time.
        /// </summary>
        /// <param name="action">The Action to perform every loop. Put your program code here.</param>
        /// <param name="repeatMessage">The message to show the user at the end of each loop, 
        /// to ask if they want to keep looping or exit.</param>
        /// <param name="exitMessage">The message to show the user when exiting the program.</param>
        public static void LoopProgram(
            Action action,
            string repeatMessage = "To run again, press 'y', to stop press 'Esc'",
            string exitMessage = "Exiting program...")
        {
            while (true)
            {
                action();

                Console.WriteLine();
                Console.WriteLine(repeatMessage);
                

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Escape || char.ToLower(input.KeyChar) != 'y')
                {
                    Console.WriteLine(exitMessage);
                    break;
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
}