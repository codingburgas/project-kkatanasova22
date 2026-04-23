using System.ComponentModel.DataAnnotations;

namespace LibraryManager.DTOs.Loan
{
    public class LoanCreateDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; } // Идва от ApplicationUser (Guid като string)

        [Required]
        [Display(Name = "Duration in Days")]
        [Range(1, 30, ErrorMessage = "You can borrow a book for 1 to 30 days.")]
        public int DurationDays { get; set; } = 14; // По подразбиране 2 седмици
    }
}
