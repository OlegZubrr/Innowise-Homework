namespace LibraryManagementSystem.DTOs.Book;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}