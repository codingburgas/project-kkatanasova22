using Microsoft.EntityFrameworkCore; // Фиксира ToListAsync и FirstOrDefaultAsync
using LibraryManager.Data;
using LibraryManager.DTOs.Book;
using LibraryManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using LibraryManager.Services.Contracts;

namespace LibraryManager.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookListDto>> GetAllBooksAsync()
        {
            return await _context.Books
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<BookDetailsDto?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => b.Id == id)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    AuthorName = b.Author.Name,
                    CategoryName = b.Category.Name, // Изисква свойство CategoryName в DTO
                    CreatedAt = b.CreatedAt         // Изисква свойство CreatedAt в DTO
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateBookAsync(BookCreateDto model)
        {
            var book = new Book
            {
                Title = model.Title,
                ISBN = model.ISBN,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId, // Изисква свойство CategoryId в DTO
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task<bool> UpdateBookAsync(int id, BookUpdateDto model)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) return false;

            book.Title = model.Title;
            book.ISBN = model.ISBN;
            book.AuthorId = model.AuthorId;
            book.CategoryId = model.CategoryId; // Изисква свойство CategoryId в DTO

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BookListDto>> SearchBooksByTitleOrAuthorAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return await GetAllBooksAsync();

            string query = searchQuery.ToLower();

            return await _context.Books
                .Where(b => b.Title.ToLower().Contains(query) ||
                            b.Author.Name.ToLower().Contains(query))
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookListDto>> GetBooksByGenreAsync(string genre)
        {
            return await _context.Books
                .Where(b => b.Category.Name == genre)
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return book?.IsAvailable ?? false;
        }
    }
}