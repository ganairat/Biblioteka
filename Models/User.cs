using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestUser.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public List<UserBook> UserBooks { get; set; }
        public User()
        {
            UserBooks = new List<UserBook>();
        }

    }
}
