using LibraryManager.DTOs.Book;
using LibraryManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService,
            ICategoryService categoryService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        // GET: Books
        [AllowAnonymous] // Всеки може да разглежда каталога
        public async Task<IActionResult> Index(string searchQuery, string genre)
        {
            var books = await _bookService.SearchBooksByTitleOrAuthorAsync(searchQuery);

            if (!string.IsNullOrEmpty(genre))
            {
                books = await _bookService.GetBooksByGenreAsync(genre);
            }

            return View(books);
        }

        // GET: Books/Create
        [Authorize(Roles = "Librarian,Admin")] // Само админи добавят книги
        public async Task<IActionResult> Create()
        {
            // Подготвяме падащите менюта за Автори и Категории
            var authors = await _authorService.GetAllAuthorsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Authors = new SelectList(authors, "Id", "Name");
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            await _bookService.CreateBookAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}