namespace LibraryManager.DTOs.Book
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string AuthorName { get; set; } = null!;

        // Добави тези двете:
        public string CategoryName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
