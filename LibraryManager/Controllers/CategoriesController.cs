using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    using LibraryManager.DTOs.Category;
    using LibraryManager.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin,Librarian")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        public async Task<IActionResult> Index() => View(await _categoryService.GetAllCategoriesAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryInputDto model)
        {
            if (ModelState.IsValid) { await _categoryService.CreateCategoryAsync(model); return RedirectToAction(nameof(Index)); }
            return View(model);
        }
    }
}
