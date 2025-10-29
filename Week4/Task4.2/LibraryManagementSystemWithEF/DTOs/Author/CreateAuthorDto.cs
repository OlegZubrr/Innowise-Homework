namespace LibraryManagementSystemWithEF.DTOs.Author;

public class CreateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}