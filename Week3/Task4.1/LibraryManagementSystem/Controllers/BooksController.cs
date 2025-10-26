using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

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
    public IActionResult Create([FromBody] CreateBookDto bookDto)
    {
        var book = _bookService.Create(bookDto);

        if (book is null)
            return BadRequest("Invalid data or author does not exist");

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CreateBookDto bookDto)
    {
        var updatedBook = _bookService.Update(id, bookDto);

        return updatedBook ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletedBook = _bookService.Delete(id);
        return deletedBook ? NoContent() : NotFound();
    }
}