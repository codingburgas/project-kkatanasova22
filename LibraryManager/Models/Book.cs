using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Foreign Keys
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
