using LibraryManager.Data;
using LibraryManager.DTOs.Author;
using LibraryManager.Models;
using LibraryManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services.Implementations
{

    
        public class AuthorsService : IAuthorService
        {
            private readonly ApplicationDbContext _context;

            public AuthorsService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<AuthorListDto>> GetAllAuthorsAsync()
            {
                return await _context.Authors
                    .Select(a => new AuthorListDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    })
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }

            public async Task<AuthorDetailsDto?> GetAuthorByIdAsync(int id)
            {
                return await _context.Authors
                    .Where(a => a.Id == id)
                    .Select(a => new AuthorDetailsDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        BooksCount = a.Books.Count,
                        CreatedAt = a.CreatedAt
                    })
                    .FirstOrDefaultAsync();
            }

            public async Task<int> CreateAuthorAsync(AuthorInputDto model)
            {
                var author = new Author
                {
                    Name = model.Name,
                    CreatedAt = DateTime.UtcNow // Свойството от BaseEntity
                };

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                return author.Id;
            }

            public async Task<bool> UpdateAuthorAsync(int id, AuthorInputDto model)
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null) return false;

                author.Name = model.Name;

                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteAuthorAsync(int id)
            {
                // Използваме Include, за да заредим книгите и да проверим дали авторът има такива
                var author = await _context.Authors
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(a => a.Id == id);

                // БИЗНЕС ПРАВИЛО: Не позволяваме изтриване на автор, който има книги в каталога
                if (author == null || author.Books.Any())
                {
                    return false;
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> AuthorExistsAsync(int id)
            {
                return await _context.Authors.AnyAsync(a => a.Id == id);
            }

            
        }
    }

