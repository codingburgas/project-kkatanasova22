
using LibraryManager.DTOs.Book;

namespace LibraryManagers.Core.Contracts
{
    public interface IBookService
    {
        // CRUD Операции
        Task<IEnumerable<BookListDto>> GetAllBooksAsync();

        Task<BookDetailsDto?> GetBookByIdAsync(int id);

        Task<int> CreateBookAsync(BookCreateDto model);

        Task<bool> UpdateBookAsync(int id, BookUpdateDto model);

        Task<bool> DeleteBookAsync(int id);

        // Бизнес логика (LINQ заявки)
        Task<IEnumerable<BookListDto>> SearchBooksByTitleOrAuthorAsync(string searchQuery);

        Task<IEnumerable<BookListDto>> GetBooksByGenreAsync(string genre);

        Task<bool> IsBookAvailableAsync(int bookId);
    }
}