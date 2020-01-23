using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestUser.Models;

namespace TestUser.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<TestUser.Models.User> User { get; set; }
        public DbSet<TestUser.Models.Book> Book { get; set; }
        public DbSet<TestUser.Models.UserBook> UserBook { get; set; }
              
        
        }
}
