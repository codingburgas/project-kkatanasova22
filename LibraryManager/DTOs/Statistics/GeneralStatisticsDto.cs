namespace LibraryManager.DTOs.Statistics
{
    public class GeneralStatisticsDto
    {
        public int TotalBooks { get; set; }
        public int ActiveLoans { get; set; }
        public double AverageLoanDurationDays { get; set; }
    }
}
