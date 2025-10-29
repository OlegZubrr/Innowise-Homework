namespace LibraryManagementSystemWithEF.DTOs.Author;

public class UpdateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}