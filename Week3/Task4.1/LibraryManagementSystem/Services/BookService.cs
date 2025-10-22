using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService
{
    public IEnumerable<Book> GetAll()
    {
        return LibraryData.Books;
    }

    public Book? GetById(int id)
    {
        return LibraryData.Books.FirstOrDefault(x => x.Id == id);
    }

    public Book? Create(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            return null;

        var isAuthorExists = LibraryData.Authors.Any(x => x.Id == book.AuthorId);

        if (!isAuthorExists)
            return null;

        if (book.PublishedYear > DateTime.Now.Year)
            return null;

        book.Id = LibraryData.Books.Any()
            ? LibraryData.Books.Max(a => a.Id) + 1
            : 1;
        LibraryData.Books.Add(book);
        return book;
    }

    public bool Update(Book updateBook)
    {
        var existingBook = LibraryData.Books.FirstOrDefault(a => a.Id == updateBook.Id);

        if (existingBook is null) return false;

        if (string.IsNullOrWhiteSpace(updateBook.Title)) return false;

        var isAuthorExists = LibraryData.Authors.Any(x => x.Id == updateBook.AuthorId);

        if (!isAuthorExists) return false;

        if (updateBook.PublishedYear > DateTime.Now.Year)
            return false;

        existingBook.Title = updateBook.Title;
        existingBook.PublishedYear = updateBook.PublishedYear;
        existingBook.AuthorId = updateBook.AuthorId;
        return true;
    }

    public bool Delete(int id)
    {
        var book = LibraryData.Books.FirstOrDefault(a => a.Id == id);

        if (book is null) return false;

        LibraryData.Books.Remove(book);
        return true;
    }
}