using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public IEnumerable<Author> GetAll()
    {
        return _authorRepository.GetAll();
    }

    public Author? GetById(int id)
    {
        return _authorRepository.GetById(id);
    }

    public Author? Create(CreateAuthorDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        return _authorRepository.Create(author);
    }

    public bool Update(int id, CreateAuthorDto authorDto)
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
        return _authorRepository.Delete(id);
    }
}