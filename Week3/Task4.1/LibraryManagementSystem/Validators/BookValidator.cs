using FluentValidation;
using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Validators;

public class BookValidator : AbstractValidator<CreateBookDto>
{
    public BookValidator(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(2).WithMessage("Title too short")
            .MaximumLength(50).WithMessage("Title too long");

        RuleFor(b => b.PublishedYear)
            .LessThan(DateTime.Now.Year).WithMessage("Published year cannot be in the future");

        RuleFor(b => b.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.")
            .Must(id => authorRepository.GetById(id) is not null)
            .WithMessage("Author with given ID does not exist.");

        RuleFor(b => b)
            .Must(b => !bookRepository.isExists(b.Title, b.PublishedYear))
            .WithMessage("Book with this title and published year already exists.");
    }
}