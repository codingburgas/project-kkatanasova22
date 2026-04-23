using System.ComponentModel.DataAnnotations;

namespace LibraryManager.DTOs.Book
{
    // За създаване на нова книга
    public class BookCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Range(1500, 2100)]
        public int PublicationYear { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
