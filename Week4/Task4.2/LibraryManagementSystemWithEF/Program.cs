using FluentValidation;
using LibraryManagementSystemWithEF.Data;
using LibraryManagementSystemWithEF.Repositories.Abstractions;
using LibraryManagementSystemWithEF.Repositories.Implementations;
using LibraryManagementSystemWithEF.Services.Abstractions;
using LibraryManagementSystemWithEF.Services.Implementations;
using LibraryManagementSystemWithEF.Utils;
using LibraryManagementSystemWithEF.Validators.Author;
using LibraryManagementSystemWithEF.Validators.Book;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); });

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookValidator>();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

builder.Services.AddScoped<IAuthorRepository, EfAuthorRepository>();
builder.Services.AddScoped<IBookRepository, EfBookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    LibraryContext.Initialize(context);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();