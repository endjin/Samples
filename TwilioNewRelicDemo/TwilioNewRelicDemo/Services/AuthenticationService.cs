namespace TwilioNewRelicDemo.Services
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using TwilioNewRelicDemo.Models;

    #endregion

    public class AuthenticationService
    {
        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, Pin = "123", Name = "Pascal" },
            new User { Id = 2, Pin = "234", Name = "Mike" },
            new User { Id = 3, Pin = "345", Name = "Howard" },
        };

        public static bool Authenticate(string pin)
        {
            return Users.Any(user => user.Pin == pin);
        }

        public static User GetUser(string pin)
        {
            return Users.Single(user => user.Pin == pin);
        }
    }
}