using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestUser.Data;
using TestUser.Models;
using TestUser.Service;

namespace TestUser.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Books

        public async Task<IActionResult> Index()
        {
            var books = _context.Book.Include(u => u.UserBooks);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View(books);
        }
        public IActionResult AddBooks()
        {

            var b = new List<Book>
            {
                new Book{ Name = "Евгений Онегин", Author="Пушкин", Genre="Роман"},
                new Book{ Name = "Капитанская дочка", Author="Пушкин", Genre="Роман"},
                new Book{ Name = "Мертвый души", Author="Гоголь", Genre="Поэма"},
                new Book{ Name = "Ревизор", Author="Гоголь", Genre="Комедия"},
                new Book{ Name = "Война и мир", Author="Толстой", Genre="Роман"},
                new Book{ Name = "Живой труп", Author="Толстой", Genre="Пьеса "}
            };
            _context.Book.AddRange(b);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> ShowByAuthor(string author)
        {
            if (author == null)
            {
                return NotFound();
            }

            var books = _context.Book.Where(b => b.Author == author).Include(b => b.UserBooks).ToList();

            if (books == null)
            {
                return NotFound();
            }
            ViewBag.Books = books;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }
        public async Task<IActionResult> ShowByGenre(string genre)
        {
            if (genre == null)
            {
                return NotFound();
            }

            var books = _context.Book.Where(b => b.Genre == genre).Include(b => b.UserBooks).ToList();

            if (books == null)
            {
                return NotFound();
            }
            ViewBag.Books = books;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }
        public async Task<IActionResult> ShowByTaked(bool taked)
        {
            var books = _context.Book.Where(b => b.IsTaken == taked).Include(b=>b.UserBooks).ToList();

            if (books == null)
            {
                return NotFound();
            }
            ViewBag.Books = books;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }
    }

}
