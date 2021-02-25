using ConsoleApp1.DatabaseFile.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class AuthService
    {
        private UserRepository _userRepository = new UserRepository();

        public User Login()
        {
            Console.WriteLine();
            Console.Write("Enter your name: ");
            var userName = Console.ReadLine();

            var userByName = _userRepository
                .GetAll()
                .SingleOrDefault(x => x.Name == userName);
            
            if (userByName == null)
            {
                return Register(userName);
            }

            Console.WriteLine("Enter password: ");
            var pass = Console.ReadLine();

            return userByName.Password == pass 
                ? userByName
                : null;
        }

        private User Register(string userName)
        {
            Console.WriteLine($"We not found user with name '{userName}'. So we create new user");

            Console.WriteLine("Enter password: ");
            var pass = Console.ReadLine();

            var user = new User()
            {
                Name = userName,
                Password = pass
            };
            
            _userRepository.Save(user);

            return user;
        }
    }
}
