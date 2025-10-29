namespace LibraryManagementSystemWithEF.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystemWithEF.Models;

public class LibraryContext:DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne()
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "J.K. Rowling", DateOfBirth = new DateOnly(1965, 7, 31) },
            new Author { Id = 2, Name = "George Orwell", DateOfBirth = new DateOnly(1903, 6, 25) });

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Harry Potter", PublishedYear = 1997, AuthorId = 1 },
            new Book { Id = 2, Title = "1984", PublishedYear = 1949, AuthorId = 2 });
    }
    
    public static void Initialize(LibraryContext context)
    {
        context.Database.Migrate();
    }
    
}