using ConsoleApp1.DatabaseFile.Repository;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var guessNumber = new GuessNumber();
            guessNumber.Play();
        }

        /// <summary>
        /// Request data from user while he enter a number
        /// </summary>
        /// <returns>Return a number which entered by user</returns>
        public static int ReadNumberFromConsole()
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
