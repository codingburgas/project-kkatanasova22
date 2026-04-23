namespace LibraryManager.DTOs.Book
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
