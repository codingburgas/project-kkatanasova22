namespace LibraryManager.DTOs.Author
{
    public class AuthorDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BooksCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
