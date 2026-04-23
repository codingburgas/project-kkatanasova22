namespace LibraryManager.DTOs.Statistics
{
    public class OverdueReaderDto
    {
        public string ReaderName { get; set; } = null!;
        public string BookTitle { get; set; } = null!;
        public int DaysOverdue { get; set; }
    }
}
