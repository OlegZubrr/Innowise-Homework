using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public IEnumerable<ReadAuthorDto> GetAll()
    {
        return _authorRepository.GetAll()
            .Select(a => new ReadAuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth
            });
    }

    public ReadAuthorDto? GetById(int id)
    {
        var author = _authorRepository.GetById(id);
        if (author == null)
            return null;

        return new ReadAuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public ReadAuthorDto? Create(CreateAuthorDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        var createdAuthor = _authorRepository.Create(author);

        return new ReadAuthorDto
        {
            Id = createdAuthor.Id,
            Name = createdAuthor.Name,
            DateOfBirth = createdAuthor.DateOfBirth
        };
    }

    public bool Update(int id, UpdateAuthorDto authorDto)
    {
        var existingAuthor = _authorRepository.GetById(id);
        if (existingAuthor == null)
            return false;

        existingAuthor.Name = authorDto.Name;
        existingAuthor.DateOfBirth = authorDto.DateOfBirth;

        return _authorRepository.Update(existingAuthor);
    }

    public bool Delete(int id)
    {
        var author = _authorRepository.GetById(id);
        if (author == null)
            return false;

        return _authorRepository.Delete(id);
    }
}