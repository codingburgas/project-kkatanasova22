using LibraryManager.DTOs.Author;

namespace LibraryManager.Services.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorListDto>> GetAllAuthorsAsync();

        Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id);

        Task<int> CreateAuthorAsync(AuthorInputDto model);

        Task<bool> UpdateAuthorAsync(int id, AuthorInputDto model);

        Task<bool> DeleteAuthorAsync(int id);

        // Помощен метод за проверка дали авторът съществува
        Task<bool> AuthorExistsAsync(int id);
    }
}
