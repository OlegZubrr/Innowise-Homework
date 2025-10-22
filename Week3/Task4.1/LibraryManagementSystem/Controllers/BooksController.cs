using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _bookService.GetAll();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookService.GetById(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    {
        var createdBook = _bookService.Create(book);
        if (createdBook == null)
            return BadRequest("Invalid data or author not found");

        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book book)
    {
        if (id != book.Id) return BadRequest("ID mismatch");

        var isUpdated = _bookService.Update(book);

        if (!isUpdated)
            return BadRequest("update failed");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _bookService.Delete(id);

        if (!isDeleted) return NotFound();

        return NoContent();
    }
}