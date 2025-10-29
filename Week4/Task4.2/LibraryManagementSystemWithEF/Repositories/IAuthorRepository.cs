using LibraryManagementSystemWithEF.Models;

namespace LibraryManagementSystemWithEF.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author?> CreateAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(int id);
    Task<bool> IsExistsAsync(string name, DateOnly dateOfBirth);

    Task<IEnumerable<Author>> GetAuthorsByBookCountAsync(int bookCount);

    Task<IEnumerable<Author>> FindAuthorsByNameAsync(string namePart);
}