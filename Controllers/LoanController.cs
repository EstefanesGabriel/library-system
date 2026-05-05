using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.ViewModels;
using SistemaBiblioteca.ViewModels.Loans;

namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    public class LoanController : ControllerBase
    {
        [HttpGet("v1/loans")]
        public async Task<IActionResult> GetAsync(
            [FromServices] DataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10
        )
        {
            try
            {
                var loans = await context.Loans
                    .AsNoTracking()
                    .Include(x => x.Book)
                    .Include(x => x.User)
                    .Select(x => new ListLoanViewModel
                    {
                        Id = x.Id,
                        BookTitle = x.Book.Title,
                        UserName = x.User.FullName,
                        LoanDate = x.LoanDate,
                        DueDate = x.DueDate,
                        ReturnedAt = x.ReturnedAt,
                        LoanStatus = x.LoanStatus.ToString()
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<ListLoanViewModel>>(loans));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }
    }
}