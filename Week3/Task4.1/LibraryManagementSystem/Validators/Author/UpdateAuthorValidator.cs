using FluentValidation;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Validators.Author;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorValidator(IAuthorRepository authorRepository)
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name too short")
            .MaximumLength(50).WithMessage("Name too long");

        RuleFor(a => a.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Date of birth cannot be in the future");

        RuleFor(a => a)
            .Must(a => !authorRepository.isExists(a.Name, a.DateOfBirth))
            .WithMessage("Author with this name and date of birth already exists.");
    }
}