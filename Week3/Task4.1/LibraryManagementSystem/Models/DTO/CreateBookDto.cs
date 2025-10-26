namespace LibraryManagementSystem.Models.DTO;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}