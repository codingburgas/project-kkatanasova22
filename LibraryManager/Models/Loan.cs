using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Models
{
    public class Loan : BaseEntity
    {
        [Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }

        [Required]
        public string UserId { get; set; }

        // Релация към вградения Identity потребител
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }

        [Required]
        [Display(Name = "Date Borrowed")]
        public DateTime LoanDate { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Date Returned")]
        public DateTime? ReturnDate { get; set; }

        // Изчислено свойство за проверка на просрочие
        [NotMapped]
        public bool IsOverdue => !ReturnDate.HasValue && DateTime.UtcNow > DueDate;
    }
}
