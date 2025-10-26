using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    public IEnumerable<Book> GetAll()
    {
        return LibraryData.Books;
    }

    public Book? GetById(int id)
    {
        return LibraryData.Books.FirstOrDefault(x => x.Id == id);
    }

    public Book Create(Book book)
    {
        book.Id = LibraryData.Books.Any()
            ? LibraryData.Books.Max(a => a.Id) + 1
            : 1;
        LibraryData.Books.Add(book);
        return book;
    }

    public bool Update(Book updateBook)
    {
        var existingBook = GetById(updateBook.Id);
        if (existingBook == null)
            return false;

        existingBook.Title = updateBook.Title;
        existingBook.AuthorId = updateBook.AuthorId;
        existingBook.PublishedYear = updateBook.PublishedYear;
        return true;
    }

    public bool Delete(int id)
    {
        var book = GetById(id);
        if (book == null)
            return false;
        LibraryData.Books.Remove(book);
        return true;
    }


    public bool isExists(string title, int PublishedYear)
    {
        return LibraryData.Books.Any(b => b.Title == title && b.PublishedYear == PublishedYear);
    }
}