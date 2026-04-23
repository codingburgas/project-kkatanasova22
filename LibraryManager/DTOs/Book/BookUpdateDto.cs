using System.ComponentModel.DataAnnotations;

namespace LibraryManager.DTOs.Book
{
    // За редактиране на съществуваща книга
    public class BookUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string ISBN { get; set; } = null!;
        [Required]
        public int AuthorId { get; set; }

        // Добави това:
        [Required]
        public int CategoryId { get; set; }
    }
}
