using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<Book> GetAll()
    {
        return _bookRepository.GetAll();
    }

    public Book? GetById(int id)
    {
        return _bookRepository.GetById(id);
    }

    public Book? Create(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId,
            PublishedYear = bookDto.PublishedYear
        };
        return _bookRepository.Create(book);
    }

    public bool Update(int id, CreateBookDto bookDto)
    {
        var existingBook = _bookRepository.GetById(id);
        if (existingBook == null)
            return false;

        existingBook.Title = bookDto.Title;
        existingBook.AuthorId = bookDto.AuthorId;
        existingBook.PublishedYear = bookDto.PublishedYear;

        return _bookRepository.Update(existingBook);
    }

    public bool Delete(int id)
    {
        return _bookRepository.Delete(id);
    }
}