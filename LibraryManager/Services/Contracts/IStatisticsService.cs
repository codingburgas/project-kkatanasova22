using LibraryManager.DTOs.Statistics;

namespace LibraryManager.Services.Contracts
{
    public interface IStatisticsService
    {
        Task<IEnumerable<BookReportDto>> GetTop5BorrowedBooksAsync();
        Task<IEnumerable<OverdueReaderDto>> GetOverdueReadersAsync();
        Task<GeneralStatisticsDto> GetGeneralStatisticsAsync();
    }
}
