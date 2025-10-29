using FluentValidation;
using LibraryManagementSystemWithEF.DTOs.Author;
using LibraryManagementSystemWithEF.Repositories.Abstractions;

namespace LibraryManagementSystemWithEF.Validators.Author;

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
            .MustAsync(async (a, cancellation) =>
                !await authorRepository.IsExistsAsync(a.Name, a.DateOfBirth))
            .WithMessage("Author with this name and date of birth already exists.");
    }
}