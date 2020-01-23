using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestUser.Models
{
    public class UserBook
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
    }
}
