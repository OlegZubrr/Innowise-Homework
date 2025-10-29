using LibraryManagementSystemWithEF.Data;
using LibraryManagementSystemWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemWithEF.Repositories;

public class EfAuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;

    public EfAuthorRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author?> CreateAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author updateAuthor)
    {
        var existing = await _context.Authors.FindAsync(updateAuthor.Id);

        existing.Name = updateAuthor.Name;
        existing.DateOfBirth = updateAuthor.DateOfBirth;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _context.Authors.FindAsync(id);

        _context.Authors.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(string name, DateOnly dateOfBirth)
    {
        return await _context.Authors.AnyAsync(a => a.Name == name && a.DateOfBirth == dateOfBirth);
    }

    public async Task<IEnumerable<Author>> GetAuthorsByBookCountAsync(int bookCount)
    {
        return await _context.Authors
            .Where(a => a.Books.Count == bookCount)
            .ToListAsync();
    }


    public async Task<IEnumerable<Author>> FindAuthorsByNameAsync(string namePart)
    {
        return await _context.Authors
            .Where(a => a.Name.Contains(namePart))
            .ToListAsync();
    }
}