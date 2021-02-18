using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the game guess number.");

            Console.WriteLine("First user");
            var theNumber = ReadNumberFromConsole();

            Console.Clear();

            Console.WriteLine("Hi Second user.");
            int guessNumber;
            int attempt = 0;
            bool isWin = false;
            do
            {
                Console.WriteLine($"Try to guess the number. Your attempt №{++attempt}");

                guessNumber = ReadNumberFromConsole();
                if (guessNumber < theNumber)
                {
                    Console.WriteLine("you number LESS than The number");
                }
                else if (guessNumber > theNumber)
                {
                    Console.WriteLine("you number MORE than The number");
                }
                else
                {
                    isWin = true;
                }
            } while (guessNumber != theNumber && attempt < 7);

            Console.WriteLine(isWin ? "You win" : "Loose!");
        }

        /// <summary>
        /// Request data from user while he enter a number
        /// </summary>
        /// <returns>Return a number which entered by user</returns>
        private static int ReadNumberFromConsole()
        {
            int theNumber;
            string str;
            do
            {
                Console.WriteLine("Enter the number: ");
                str = Console.ReadLine();
                //Console.WriteLine($"The line \"{str}\" not number. Enter the number: ");
            } while (!int.TryParse(str, out theNumber));

            return theNumber;
        }
    }
}
