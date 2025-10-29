using LibraryManagementSystemWithEF.Data;
using LibraryManagementSystemWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemWithEF.Repositories;

public class EfBookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public EfBookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book?> CreateAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book updateBook)
    {
        var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == updateBook.Id);

        existingBook.Title = updateBook.Title;
        existingBook.PublishedYear = updateBook.PublishedYear;

        if (existingBook.AuthorId != updateBook.AuthorId)
        {
            var newAuthor = await _context.Authors.FindAsync(updateBook.AuthorId);

            existingBook.AuthorId = newAuthor.Id;
            existingBook.Author = newAuthor;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _context.Books.FindAsync(id);

        _context.Books.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(string title, int PublishedYear)
    {
        return await _context.Books.AnyAsync(b => b.Title == title && b.PublishedYear == PublishedYear);
    }

    public async Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
    {
        return await _context.Books
            .Where(b => b.PublishedYear > year)
            .ToListAsync();
    }
}