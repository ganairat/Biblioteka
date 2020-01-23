using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestUser.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public List<UserBook> UserBooks { get; set; }
        public bool IsTaken { get; set; }
        public Book()
        {
            UserBooks = new List<UserBook>();
        }
    }
}
