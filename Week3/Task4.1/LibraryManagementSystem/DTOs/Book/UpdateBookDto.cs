namespace LibraryManagementSystem.DTOs.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}