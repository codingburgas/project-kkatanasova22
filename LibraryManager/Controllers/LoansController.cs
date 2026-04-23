using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    using LibraryManager.DTOs.Loan;
    using LibraryManager.Services.Contracts;
    using LibraryManagers.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService) => _loanService = loanService;

        // Списък със заемания за текущия потребител
        public async Task<IActionResult> MyLoans()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _loanService.GetUserLoansAsync(userId));
        }

        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Index() => View(await _loanService.GetAllActiveLoansAsync());

        [HttpPost]
        public async Task<IActionResult> Borrow(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new LoanCreateDto { BookId = bookId, UserId = userId, DurationDays = 14 };

            var success = await _loanService.BorrowBookAsync(model);
            if (!success) return BadRequest("Book is not available.");

            return RedirectToAction(nameof(MyLoans));
        }

        [HttpPost]
        public async Task<IActionResult> Return(int loanId)
        {
            await _loanService.ReturnBookAsync(loanId);
            return RedirectToAction(nameof(MyLoans));
        }
    }
}
