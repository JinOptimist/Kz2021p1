using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ConsoleApp1.DatabaseFile.Repository
{
    public class UserRepository
    {
        public string JsonPath
        {
            get
            {
                var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var appPath = Path.GetDirectoryName(dllPath);
                return Path.Combine(appPath, "Users.json");
            }
        }

        public List<User> GetAll()
        {
            var json = File.ReadAllText(JsonPath);

            var users = JsonSerializer.Deserialize<List<User>>(json);

            return users;
        }

        public void Save(User user)
        {
            var allUser = GetAll();
            allUser.Add(user);
            var jsonString = JsonSerializer.Serialize(allUser);
            File.WriteAllText(JsonPath, jsonString);
        }
    }
}
