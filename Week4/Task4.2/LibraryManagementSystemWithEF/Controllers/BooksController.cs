using FluentValidation;
using LibraryManagementSystemWithEF.DTOs.Book;
using LibraryManagementSystemWithEF.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemWithEF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;
    private readonly IValidator<CreateBookDto> _createValidator;
    private readonly IValidator<UpdateBookDto> _updateValidator;

    public BooksController(
        BookService bookService,
        IValidator<CreateBookDto> createValidator,
        IValidator<UpdateBookDto> updateValidator)
    {
        _bookService = bookService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto bookDto)
    {
        var validationResult = await _createValidator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var book = await _bookService.CreateAsync(bookDto);

        if (book is null)
            return BadRequest("Invalid data or author does not exist");

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto bookDto)
    {
        var validationResult = await _updateValidator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var updatedBook = await _bookService.UpdateAsync(id, bookDto);

        return updatedBook ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedBook = await _bookService.DeleteAsync(id);
        return deletedBook ? NoContent() : NotFound();
    }

    [HttpGet("published-after/{year}")]
    public async Task<IActionResult> GetBooksPublishedAfter(int year)
    {
        var books = await _bookService.GetBooksPublishedAfterAsync(year);
        return Ok(books);
    }
}