using LibraryManagementSystemWithEF.DTOs.Author;
using LibraryManagementSystemWithEF.Models;
using LibraryManagementSystemWithEF.Repositories;

namespace LibraryManagementSystemWithEF.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public AuthorService(
        IAuthorRepository authorRepository
        , IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<ReadAuthorDto>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();

        return authors.Select(a => new ReadAuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth
        });
    }

    public async Task<ReadAuthorDto?> GetByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return null;

        return new ReadAuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public async Task<ReadAuthorDto?> CreateAsync(CreateAuthorDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        var createdAuthor = await _authorRepository.CreateAsync(author);

        return new ReadAuthorDto
        {
            Id = createdAuthor.Id,
            Name = createdAuthor.Name,
            DateOfBirth = createdAuthor.DateOfBirth
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateAuthorDto authorDto)
    {
        var existingAuthor = await _authorRepository.GetByIdAsync(id);
        if (existingAuthor == null)
            return false;

        existingAuthor.Name = authorDto.Name;
        existingAuthor.DateOfBirth = authorDto.DateOfBirth;

        await _authorRepository.UpdateAsync(existingAuthor);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            return false;

        await _authorRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ReadAuthorDto>> GetAuthorsByBookCountAsync(int bookCount)
    {
        var authors = await _authorRepository.GetAuthorsByBookCountAsync(bookCount);

        return authors.Select(a => new ReadAuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth
        });
    }

    public async Task<IEnumerable<ReadAuthorDto>> FindAuthorsByNameAsync(string namePart)
    {
        var authors = await _authorRepository.FindAuthorsByNameAsync(namePart);

        return authors.Select(a => new ReadAuthorDto
        {
            Id = a.Id,
            Name = a.Name,
            DateOfBirth = a.DateOfBirth
        });
    }
}