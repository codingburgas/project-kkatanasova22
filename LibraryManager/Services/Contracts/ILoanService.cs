using LibraryManager.DTOs.Loan;

namespace LibraryManager.Services.Contracts
{
    public interface ILoanService
    {
        // Основна логика за заемане
        Task<bool> BorrowBookAsync(LoanCreateDto model);

        // Логика за връщане на книга
        Task<bool> ReturnBookAsync(int loanId);

        // Списък с всички активни заемания
        Task<IEnumerable<LoanListDto>> GetAllActiveLoansAsync();

        // Списък със заеманията на конкретен потребител (за Reader ролята)
        Task<IEnumerable<LoanListDto>> GetUserLoansAsync(string userId);

        // Проверка дали книгата е свободна
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}
