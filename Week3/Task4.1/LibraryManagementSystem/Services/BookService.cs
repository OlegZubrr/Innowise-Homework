using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<ReadBookDto> GetAll()
    {
        return _bookRepository.GetAll()
            .Select(a => new ReadBookDto
            {
                Id = a.Id,
                Title = a.Title,
                PublishedYear = a.PublishedYear,
                AuthorId = a.AuthorId
            });
    }

    public ReadBookDto? GetById(int id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            return null;

        return new ReadBookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }

    public ReadBookDto? Create(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId,
            PublishedYear = bookDto.PublishedYear
        };
        var createdBook = _bookRepository.Create(book);

        return new ReadBookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            AuthorId = createdBook.AuthorId,
            PublishedYear = createdBook.PublishedYear
        };
    }

    public bool Update(int id, UpdateBookDto bookDto)
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
        var book = _bookRepository.GetById(id);
        if (book == null)
            return false;

        return _bookRepository.Delete(id);
    }
}