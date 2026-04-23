using LibraryManager.Data;
using LibraryManager.DTOs.Loan;
using LibraryManager.Models;
using LibraryManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BorrowBookAsync(LoanCreateDto model)
        {
            // 1. Проверка дали книгата съществува и дали е свободна
            var book = await _context.Books.FindAsync(model.BookId);
            if (book == null || !book.IsAvailable) return false;

            // 2. Създаване на записа за заемане
            var loan = new Loan
            {
                BookId = model.BookId,
                UserId = model.UserId,
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(model.DurationDays),
                ReturnDate = null,
                CreatedAt = DateTime.UtcNow
            };

            // 3. Маркираме книгата като заета
            book.IsAvailable = false;

            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null || loan.ReturnDate != null) return false;

            // 1. Маркираме датата на връщане
            loan.ReturnDate = DateTime.UtcNow;

            // 2. Правим книгата отново достъпна
            if (loan.Book != null)
            {
                loan.Book.IsAvailable = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LoanListDto>> GetAllActiveLoansAsync()
        {
            return await _context.Loans
                .Where(l => l.ReturnDate == null)
                .Select(l => new LoanListDto
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    ReaderName = l.User.FirstName + " " + l.User.LastName,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    IsReturned = false,
                    IsOverdue = DateTime.UtcNow > l.DueDate
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanListDto>> GetUserLoansAsync(string userId)
        {
            return await _context.Loans
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.LoanDate)
                .Select(l => new LoanListDto
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    ReaderName = "You",
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    IsReturned = l.ReturnDate != null,
                    IsOverdue = l.ReturnDate == null && DateTime.UtcNow > l.DueDate
                })
                .ToListAsync();
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            return await _context.Books
                .AnyAsync(b => b.Id == bookId && b.IsAvailable);
        }
    }
}
