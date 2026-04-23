using System.ComponentModel.DataAnnotations;

namespace LibraryManager.DTOs.Author
{
    public class AuthorInputDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
