using LibraryManagementSystemWithEF.Models;

namespace LibraryManagementSystemWithEF.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task <bool>IsExistsAsync(string title, int publishedYear);
    
    Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
}