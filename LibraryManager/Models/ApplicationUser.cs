using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        // Можеш да добавиш допълнителни полета, специфични за библиотеката
        [Display(Name = "Library Card Number")]
        public string? LibraryCardNumber { get; set; }

        // Релация: Един потребител може да има много заемания
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
