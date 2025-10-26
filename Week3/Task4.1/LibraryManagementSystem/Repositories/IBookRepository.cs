using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book? Create(Book book);
    bool Update(Book book);
    bool Delete(int id);

    bool isExists(string title, int PublishedYear);
}