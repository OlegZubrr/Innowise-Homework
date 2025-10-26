using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Utils;
using LibraryManagementSystem.Validators.Author;
using LibraryManagementSystem.Validators.Book;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookValidator>();

builder.Services.AddSingleton<IAuthorRepository, InMemoryAuthorRepository>();
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();