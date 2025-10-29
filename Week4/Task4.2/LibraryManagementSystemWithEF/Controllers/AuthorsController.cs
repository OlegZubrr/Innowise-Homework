using FluentValidation;
using LibraryManagementSystemWithEF.DTOs.Author;
using LibraryManagementSystemWithEF.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemWithEF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IValidator<CreateAuthorDto> _createValidator;
    private readonly IValidator<UpdateAuthorDto> _updateValidator;

    public AuthorsController(IAuthorService authorService,
        IValidator<CreateAuthorDto> createValidator,
        IValidator<UpdateAuthorDto> updateValidator)
    {
        _authorService = authorService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _authorService.GetByIdAsync(id);

        if (author == null)
            return NotFound();

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorDto authorDto)
    {
        var validationResult = await _createValidator.ValidateAsync(authorDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var author = await _authorService.CreateAsync(authorDto);

        if (author is null)
            return BadRequest("Invalid data");

        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto authorDto)
    {
        var validationResult = await _updateValidator.ValidateAsync(authorDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var updatedAuthor = await _authorService.UpdateAsync(id, authorDto);

        return updatedAuthor ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedAuthor = await _authorService.DeleteAsync(id);
        return deletedAuthor ? NoContent() : NotFound();
    }

    [HttpGet("by-book-count/{count}")]
    public async Task<IActionResult> GetAuthorsByBookCount(int count)
    {
        if (count < 0)
            return BadRequest(new { Error = "Book count cannot be negative" });

        var authors = await _authorService.GetAuthorsByBookCountAsync(count);
        return Ok(authors);
    }

    [HttpGet("search")]
    public async Task<IActionResult> FindAuthorsByName([FromQuery] string name)
    {
        var authors = await _authorService.FindAuthorsByNameAsync(name);
        return Ok(authors);
    }
}