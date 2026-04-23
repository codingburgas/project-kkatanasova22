using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
