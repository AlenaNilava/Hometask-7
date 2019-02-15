using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebProject.Entities
{
    public class User
    {
        public readonly string password;
        public readonly string login;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
