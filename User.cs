using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Student
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Post { get; set; }
        public string Special { get; set; }
        public string Ex { get; set; }

        public User(string login)
        {
            Login = login;
        }

        public User(int id, string login, string password, string post)
        {
            Id = id;
            Login = login;
            Password = password;
            Post = post;
        }
        public User(string login, string password, string post)
        {
            Login = login;
            Password = password;
            Post = post;
        }

        public void AddSpecial(string special, string ex)
        {
            Special = special;
            Ex = ex;
        }

    }
}
