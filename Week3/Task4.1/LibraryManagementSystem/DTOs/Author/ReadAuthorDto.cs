namespace LibraryManagementSystem.DTOs.Author;

public class ReadAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}