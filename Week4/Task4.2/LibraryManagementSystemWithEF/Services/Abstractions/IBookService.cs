using LibraryManagementSystemWithEF.DTOs.Book;

namespace LibraryManagementSystemWithEF.Services.Abstractions;

public interface IBookService
{
    Task<IEnumerable<ReadBookDto>> GetAllAsync();
    Task<ReadBookDto?> GetByIdAsync(int id);
    Task<ReadBookDto?> CreateAsync(CreateBookDto bookDto);
    Task<bool> UpdateAsync(int id, UpdateBookDto bookDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ReadBookDto>> GetBooksPublishedAfterAsync(int year);
}