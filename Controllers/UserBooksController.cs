using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestUser.Data;
using TestUser.Models;
using TestUser.Service;

namespace TestUser.Controllers
{
    [Authorize]
    public class UserBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserBooksController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserBook.Include(u => u.Book).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Back()
        {

            return Redirect("/books");
        }
        // GET: UserBooks/Details/5
        public async Task<IActionResult> DetailsByBookId(string bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var userBook = await _context.UserBook
                .Include(u => u.Book)
                .Include(u => u.User).Where(u=>u.BookId==bookId)
                .ToListAsync();
            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        //передвется id книги, которую берут
        public async Task<IActionResult> TakeBook(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var us = await _userManager.GetUserAsync(HttpContext.User);
            var book = _context.Book.FindAsync(id).Result;
            book.IsTaken = true;
            _context.Update(book);
            UserBook userBook = new UserBook
            {   
                BookId = id,                
                Date = DateTime.Now,                
                User = us,
                Action = "Взял"
            };

            _context.Add(userBook);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ReturnBook(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var book = _context.Book.FindAsync(id).Result;
            book.IsTaken = false;
            var us = await _userManager.GetUserAsync(HttpContext.User);
            _context.Update(book);
            UserBook userBook = new UserBook
            {
                BookId = id,
                Date = DateTime.Now,
                User = us,
                Action = "Отдал"
            };

            _context.Add(userBook);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Debtors()
        {
            var books = _context.Book.Where(b => b.IsTaken == true).Include(u => u.UserBooks);
            List<string> users = new List<string>();
            foreach(var b in books)
            {
                var ub = b.UserBooks.Last().UserId;
                var email = _context.User.First(u => u.Id == ub).Email;
                if (!users.Contains(email))
                users.Add(email);
            }
            
            ViewBag.Users = users;
            return View();
        }
    }
}
