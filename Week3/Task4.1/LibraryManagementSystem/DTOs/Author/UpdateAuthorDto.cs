namespace LibraryManagementSystem.DTOs.Author;

public class UpdateAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}