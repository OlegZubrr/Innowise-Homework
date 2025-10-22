namespace LibraryManagementSystem.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}