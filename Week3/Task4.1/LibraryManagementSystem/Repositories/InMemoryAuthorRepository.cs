using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public class InMemoryAuthorRepository : IAuthorRepository
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
        author.Id = LibraryData.Authors.Any()
            ? LibraryData.Authors.Max(a => a.Id) + 1
            : 1;
        LibraryData.Authors.Add(author);
        return author;
    }

    public bool Update(Author updateAuthor)
    {
        var existingAuthor = GetById(updateAuthor.Id);
        if (existingAuthor == null)
            return false;

        existingAuthor.Name = updateAuthor.Name;
        existingAuthor.DateOfBirth = updateAuthor.DateOfBirth;
        return true;
    }

    public bool Delete(int id)
    {
        var author = GetById(id);
        if (author == null)
            return false;
        LibraryData.Authors.Remove(author);
        return true;
    }

    public bool isExists(string name, DateOnly dateOfBirth)
    {
        return LibraryData.Authors.Any(a => a.Name == name && a.DateOfBirth == dateOfBirth);
    }
}