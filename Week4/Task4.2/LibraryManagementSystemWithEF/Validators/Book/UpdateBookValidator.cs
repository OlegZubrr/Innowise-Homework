using FluentValidation;
using LibraryManagementSystemWithEF.DTOs.Book;
using LibraryManagementSystemWithEF.Repositories.Abstractions;

namespace LibraryManagementSystemWithEF.Validators.Book;

public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookValidator(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(2).WithMessage("Title too short")
            .MaximumLength(50).WithMessage("Title too long");

        RuleFor(b => b.PublishedYear)
            .LessThan(DateTime.Now.Year).WithMessage("Published year cannot be in the future");

        RuleFor(b => b.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.")
            .MustAsync(async (id, cancellation) =>
            {
                var author = await authorRepository.GetByIdAsync(id);
                return author is not null;
            })
            .WithMessage("Author with given ID does not exist.");

        RuleFor(b => b)
            .MustAsync(async (b, cancellation) =>
                !await bookRepository.IsExistsAsync(b.Title, b.PublishedYear))
            .WithMessage("Book with this title and published year already exists.");
    }
}