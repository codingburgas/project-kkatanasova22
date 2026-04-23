namespace LibraryManager.DTOs.Book
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;

        // Липсващите полета, които View-то търси:
        public string CategoryName { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string Location { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}