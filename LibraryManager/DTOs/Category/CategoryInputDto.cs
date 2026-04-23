using System.ComponentModel.DataAnnotations;

namespace LibraryManager.DTOs.Category
{
    public class CategoryInputDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = null!;
    }
}
