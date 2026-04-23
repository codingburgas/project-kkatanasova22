using System.ComponentModel.DataAnnotations;

public class LoanCreateDto
{
    public int BookId { get; set; }

    [Required]
    public Guid UserId { get; set; }// Добави = null!; тук

    public int DurationDays { get; set; }
}