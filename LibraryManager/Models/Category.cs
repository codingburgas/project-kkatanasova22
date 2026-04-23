using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
