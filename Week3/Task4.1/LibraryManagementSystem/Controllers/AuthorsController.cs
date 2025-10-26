using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorsController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var authors = _authorService.GetAll();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var author = _authorService.GetById(id);

        if (author == null)
            return NotFound();

        return Ok(author);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateAuthorDto authorDto)
    {
        var author = _authorService.Create(authorDto);

        if (author is null)
            return BadRequest("Invalid data");

        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CreateAuthorDto authorDto)
    {
        var updatedAuthor = _authorService.Update(id, authorDto);

        return updatedAuthor ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletedAuthor = _authorService.Delete(id);
        return deletedAuthor ? NoContent() : NotFound();
    }
}