using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public interface IAuthorRepository
{
    IEnumerable<Author> GetAll();
    Author? GetById(int id);
    Author? Create(Author author);
    bool Update(Author author);
    bool Delete(int id);

    bool isExists(string name, DateOnly dateOfBirth);
}