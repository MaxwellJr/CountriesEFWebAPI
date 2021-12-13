using System;

namespace CountriesEFWebAPI.Models {
    public class User {
        public User() {
            ID = 0;
            Login = String.Empty;
            Password = String.Empty;
        }

        public User(string login, string password) {
            ID = 0;
            Login = login;
            Password = password;
        }

        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
