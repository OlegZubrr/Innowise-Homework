using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data;

public static class LibraryData
{
    public static List<Author> Authors { get; set; } = new()
    {
        new Author { Id = 1, Name = "J.K. Rowling", DateOfBirth = new DateOnly(1965, 7, 31) },
        new Author { Id = 2, Name = "George Orwell", DateOfBirth = new DateOnly(1903, 6, 25) }
    };

    public static List<Book> Books { get; set; } = new()
    {
        new Book { Id = 1, Title = "Harry Potter", PublishedYear = 1997, AuthorId = 1 },
        new Book { Id = 2, Title = "1984", PublishedYear = 1949, AuthorId = 2 }
    };
}