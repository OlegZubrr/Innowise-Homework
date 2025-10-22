using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class AuthorService
{
    public IEnumerable<Author> GetAll()
    {
        return LibraryData.Authors;
    }

    public Author? GetById(int id)
    {
        return LibraryData.Authors.FirstOrDefault(x => x.Id == id);
    }

    public Author Create(Author author)
    {
        if (string.IsNullOrWhiteSpace(author.Name))
            return null;

        var isAuthorExists = LibraryData.Authors.Any(x =>
            string.Equals(x.Name, author.Name, StringComparison.OrdinalIgnoreCase) &&
            x.DateOfBirth.Date == author.DateOfBirth.Date);


        if (isAuthorExists)
            return null;

        if (author.DateOfBirth >= DateTime.Now)
            return null;

        author.Id = LibraryData.Authors.Any()
            ? LibraryData.Authors.Max(a => a.Id) + 1
            : 1;
        LibraryData.Authors.Add(author);
        return author;
    }

    public bool Update(Author updateAuthor)
    {
        var existingAuthor = LibraryData.Authors.FirstOrDefault(a => a.Id == updateAuthor.Id);

        if (existingAuthor is null) return false;

        if (string.IsNullOrWhiteSpace(updateAuthor.Name)) return false;

        if (updateAuthor.DateOfBirth > DateTime.Now)
            return false;

        existingAuthor.Name = updateAuthor.Name;
        existingAuthor.DateOfBirth = updateAuthor.DateOfBirth;
        return true;
    }

    public bool Delete(int id)
    {
        var author = LibraryData.Authors.FirstOrDefault(a => a.Id == id);

        if (author is null) return false;
        LibraryData.Books.RemoveAll(b => b.AuthorId == id);
        LibraryData.Authors.Remove(author);
        return true;
    }
}