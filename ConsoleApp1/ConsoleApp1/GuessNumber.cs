using ConsoleApp1.DatabaseFile.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class GuessNumber
    {
        private int TheNumber { get; set; }
        private bool IsWin { get; set; }

        private User UserSecondPlayer { get; set; }

        private AuthService _authService = new AuthService();
        private UserRepository _userRepository = new UserRepository();
        

        public void Play()
        {
            Console.WriteLine("This is the game guess number.");

            FirstPlayer();

            SecondPlayer();

            CheckResult();
        }

        private void FirstPlayer()
        {
            Console.WriteLine("First user");
            var user = _authService.Login();
            if (user.LoseGameCount+ user.WinGameCount == 0)
            {
                PrintRule();
            }
            
            TheNumber = Program.ReadNumberFromConsole();
        }

        private void PrintRule()
        {
            Console.WriteLine("Rule of the guess number game ...");
        }

        private void SecondPlayer()
        {
            Console.Clear();

            Console.WriteLine("Hi Second user.");

            UserSecondPlayer = _authService.Login();

            int guessNumber;
            int attempt = 0;
            IsWin = false;
            do
            {
                Console.WriteLine($"Try to guess the number. Your attempt №{++attempt}");

                guessNumber = Program.ReadNumberFromConsole();
                if (guessNumber < TheNumber)
                {
                    Console.WriteLine("you number LESS than The number");
                }
                else if (guessNumber > TheNumber)
                {
                    Console.WriteLine("you number MORE than The number");
                }
                else
                {
                    IsWin = true;
                }
            } while (guessNumber != TheNumber && attempt < 7);

        }

        private void CheckResult()
        {
            if (IsWin)
            {
                UserSecondPlayer.WinGameCount++;
            }
            else
            {
                UserSecondPlayer.LoseGameCount++;
            }

            _userRepository.Save(UserSecondPlayer);

            Console.WriteLine(IsWin 
                ? $"You win. It's is your {UserSecondPlayer.WinGameCount}'s win" 
                : $"Loose! It's is your {UserSecondPlayer.LoseGameCount}'s loos");
        }
    }
}
