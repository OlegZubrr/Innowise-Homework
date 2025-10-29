namespace LibraryManagementSystemWithEF.DTOs.Book;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}