using LibraryManager.Data;
using LibraryManager.DTOs.Statistics;
using LibraryManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookReportDto>> GetTop5BorrowedBooksAsync()
        {
            return await _context.Loans
                .GroupBy(l => l.Book.Title)
                .Select(g => new BookReportDto
                {
                    Title = g.Key,
                    TimesBorrowed = g.Count()
                })
                .OrderByDescending(b => b.TimesBorrowed)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<OverdueReaderDto>> GetOverdueReadersAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Loans
                .Where(l => l.ReturnDate == null && l.DueDate < now)
                .Select(l => new OverdueReaderDto
                {
                    ReaderName = l.User.FirstName + " " + l.User.LastName,
                    BookTitle = l.Book.Title,
                    DaysOverdue = (now - l.DueDate).Days
                })
                .ToListAsync();
        }

        public async Task<GeneralStatisticsDto> GetGeneralStatisticsAsync()
        {
            var loansWithDuration = await _context.Loans
                .Where(l => l.ReturnDate != null)
                .Select(l => (l.ReturnDate.Value - l.LoanDate).TotalDays)
                .ToListAsync();

            return new GeneralStatisticsDto
            {
                TotalBooks = await _context.Books.CountAsync(),
                ActiveLoans = await _context.Loans.CountAsync(l => l.ReturnDate == null),
                AverageLoanDurationDays = loansWithDuration.Any() ? loansWithDuration.Average() : 0
            };
        }
    }
}
