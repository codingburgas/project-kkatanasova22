using LibraryManager.DTOs.Category;

namespace LibraryManager.Services.Contracts
{
    public interface ICategoryService
    {
        // Връща всички категории за изглед в списък
        Task<IEnumerable<CategoryListDto>> GetAllCategoriesAsync();

        // Връща детайли за конкретна категория по ID
        Task<CategoryDetailsDto?> GetCategoryByIdAsync(int id);

        // Създава нова категория и връща нейното ID
        Task<int> CreateCategoryAsync(CategoryInputDto model);

        // Обновява съществуваща категория
        Task<bool> UpdateCategoryAsync(int id, CategoryInputDto model);

        // Изтрива категория (ако няма книги в нея)
        Task<bool> DeleteCategoryAsync(int id);

        // Проверка за съществуване
        Task<bool> CategoryExistsAsync(int id);
    }
}
