using LibraryManagementSystemWithEF.DTOs.Book;
using LibraryManagementSystemWithEF.Models;
using LibraryManagementSystemWithEF.Repositories.Abstractions;
using LibraryManagementSystemWithEF.Services.Abstractions;

namespace LibraryManagementSystemWithEF.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<ReadBookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books
            .Select(a => new ReadBookDto
            {
                Id = a.Id,
                Title = a.Title,
                PublishedYear = a.PublishedYear,
                AuthorId = a.AuthorId
            });
    }

    public async Task<ReadBookDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
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

    public async Task<ReadBookDto?> CreateAsync(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId,
            PublishedYear = bookDto.PublishedYear
        };
        var createdBook = await _bookRepository.CreateAsync(book);

        return new ReadBookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            AuthorId = createdBook.AuthorId,
            PublishedYear = createdBook.PublishedYear
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookDto bookDto)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
            return false;

        existingBook.Title = bookDto.Title;
        existingBook.AuthorId = bookDto.AuthorId;
        existingBook.PublishedYear = bookDto.PublishedYear;

        await _bookRepository.UpdateAsync(existingBook);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return false;

        await _bookRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ReadBookDto>> GetBooksPublishedAfterAsync(int year)
    {
        var books = await _bookRepository.GetBooksPublishedAfterAsync(year);

        return books.Select(b => new ReadBookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishedYear = b.PublishedYear,
            AuthorId = b.AuthorId
        });
    }
}