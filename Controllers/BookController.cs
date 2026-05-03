using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.Extension;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.ViewModels;
using SistemaBiblioteca.ViewModels.Books;

namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet("v1/books")]
        public async Task<IActionResult> GetAsync(
            [FromServices] DataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10
        )
        {
            try
            {
                var count = await context.Books.AsNoTracking().CountAsync();
                var books = await context
                    .Books
                    .AsNoTracking()
                    .Select(x => new ListBookViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Author = x.Author,
                        Isbn = x.Isbn,
                        Genre = x.Genre,
                        CoverImageUrl = x.CoverImageUrl,
                        PublicationYear = x.PublicationYear,
                        StockQuantity = x.StockQuantity,
                        IsActive = x.IsActive
                    })
                    .OrderBy(x => x.Title)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    books
                }
                ));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Book>>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/books/{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var book = await context
                    .Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return NotFound(new ResultViewModel<Book>("Conteudo nao encontrado"));

                return Ok(new ResultViewModel<Book>(book));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Book>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/books/autor/{autor}")]
        public async Task<IActionResult> GetByAutorAsync(
            [FromRoute] string autor,
            [FromServices] DataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10
        )
        {
            try
            {
                var count = await context.Books.AsNoTracking().CountAsync();
                var books = await context
                    .Books
                    .AsNoTracking()
                    .Where(x => x.Author == autor)
                    .Select(x => new ListBookViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Author = x.Author,
                        Isbn = x.Isbn,
                        Genre = x.Genre,
                        CoverImageUrl = x.CoverImageUrl,
                        PublicationYear = x.PublicationYear,
                        StockQuantity = x.StockQuantity,
                        IsActive = x.IsActive
                    })
                    .OrderBy(x => x.Title)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    books
                }
                    
                ));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Book>>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/books")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateBookViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Book>(ModelState.GetErrors()));

            try
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Isbn = model.Isbn,
                    Genre = model.Genre,
                    CoverImageUrl = model.CoverImageUrl,
                    PublicationYear = model.PublicationYear,
                    StockQuantity = model.StockQuantity,
                    InstitutionId = model.InstitutionId,
                    IsActive = true

                };

                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();

                return Created($"v1/books/{book.Id}", new ResultViewModel<Book>(book));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Book>("Não foi possível incluir o livro!"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Book>("Falha interna no servidor"));
            }
        }

        [HttpPut("v1/books/{id:Guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateBookViewModel model,
            [FromServices] DataContext context
        )
        {
            try
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return NotFound(new ResultViewModel<Book>("Conteudo nao encontrado"));

                book.Title = model.Title;
                book.Author = model.Author;
                book.Isbn = model.Isbn;
                book.Genre = model.Genre;
                book.CoverImageUrl = model.CoverImageUrl;
                book.PublicationYear = model.PublicationYear;
                book.StockQuantity = model.StockQuantity;
                book.IsActive = model.IsActive;

                context.Books.Update(book);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Book>(book));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Book>("Não foi possível atualizar o livro!"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Book>("Falha interna no servidor"));
            }
        }
        [HttpDelete("v1/books/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return NotFound(new ResultViewModel<Book>("Conteudo nao encontrado"));

                context.Books.Remove(book);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Book>(book));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Book>("Não foi possível excluir o livro!"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Book>("Falha interna no servidor"));
            }
        }
    }
}