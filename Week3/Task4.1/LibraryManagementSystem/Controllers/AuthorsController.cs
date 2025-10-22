using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorService _authorService = new();

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
    public IActionResult Create([FromBody] Author author)
    {
        var createdAuthor = _authorService.Create(author);

        if (createdAuthor == null)
            return BadRequest("Author is already exists or invalid data");

        return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Author author)
    {
        if (id != author.Id) return BadRequest("ID mismatch");

        var isUpdated = _authorService.Update(author);

        if (!isUpdated)
            return BadRequest("Update failed");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _authorService.Delete(id);

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}