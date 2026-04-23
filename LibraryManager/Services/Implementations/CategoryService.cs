using LibraryManager.Data;
using LibraryManager.DTOs.Category;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Services.Contracts;

namespace LibraryManager.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryListDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryListDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<CategoryDetailsDto?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    BooksCount = c.Books.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateCategoryAsync(CategoryInputDto model)
        {
            var category = new Category
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryInputDto model)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = model.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);

            // Не трием категория, ако в нея има зачислени книги
            if (category == null || category.Books.Any())
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
