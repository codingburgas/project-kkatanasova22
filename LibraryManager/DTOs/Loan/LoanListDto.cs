namespace LibraryManager.DTOs.Loan
{
    public class LoanListDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public string ReaderName { get; set; } = null!;
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }
    }
}
