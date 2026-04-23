namespace LibraryManager.Controllers
{
    using LibraryManager.DTOs.Author;
    using LibraryManager.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin,Librarian")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService) => _authorService = authorService;

        public async Task<IActionResult> Index() => View(await _authorService.GetAllAuthorsAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorInputDto model)
        {
            if (ModelState.IsValid) { await _authorService.CreateAuthorAsync(model); return RedirectToAction(nameof(Index)); }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authorService.DeleteAuthorAsync(id);
            if (!success) TempData["Error"] = "Cannot delete author with existing books.";
            return RedirectToAction(nameof(Index));
        }
        // ПРАВИЛНО
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();

            return View(author);
        }
    }
}
