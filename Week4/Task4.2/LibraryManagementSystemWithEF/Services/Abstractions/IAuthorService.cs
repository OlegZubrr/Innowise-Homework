using LibraryManagementSystemWithEF.DTOs.Author;

namespace LibraryManagementSystemWithEF.Services.Abstractions;

public interface IAuthorService
{
    Task<IEnumerable<ReadAuthorDto>> GetAllAsync();
    Task<ReadAuthorDto?> GetByIdAsync(int id);
    Task<ReadAuthorDto?> CreateAsync(CreateAuthorDto authorDto);
    Task<bool> UpdateAsync(int id, UpdateAuthorDto authorDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ReadAuthorDto>> GetAuthorsByBookCountAsync(int bookCount);
    Task<IEnumerable<ReadAuthorDto>> FindAuthorsByNameAsync(string namePart);
}