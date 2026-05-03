using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.Extension;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.ViewModels;
using SistemaBiblioteca.ViewModels.Institutions;

namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        [HttpGet("v1/institutions")]
        public async Task<IActionResult> GetAsync(
            [FromServices] DataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10
        )
        {
            try
            {
                var count = await context.Institutions.AsNoTracking().CountAsync();
                var institutions = await context
                    .Institutions
                    .AsNoTracking()
                    .Select(x => new ListInstitutionViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TaxId = x.TaxId,
                        ContactEmail = x.ContactEmail,
                        Phone = x.Phone,
                        Address = x.Address,
                        IsActive = x.IsActive
                    })
                    .OrderBy(x => x.Name)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    institutions
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Institution>>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/institutions/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var institution = await context
                    .Institutions
                    .AsNoTracking()
                    .Select(x => new ListInstitutionViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TaxId = x.TaxId,
                        ContactEmail = x.ContactEmail,
                        Phone = x.Phone,
                        Address = x.Address,
                        IsActive = x.IsActive
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (institution == null)
                    return NotFound(new ResultViewModel<string>("Instituição não encontrada"));

                return Ok(new ResultViewModel<ListInstitutionViewModel>(institution));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/institutions")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateInstitutionViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var institution = new Institution
                {
                    Name = model.Name,
                    TaxId = model.TaxId,
                    ContactEmail = model.ContactEmail,
                    Phone = model.Phone,
                    Address = model.Address,
                    IsActive = true
                };

                await context.Institutions.AddAsync(institution);
                await context.SaveChangesAsync();

                return Created($"v1/institutions/{institution.Id}", new ResultViewModel<Institution>(institution));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível incluir. Verifique se o Documento (CNPJ) já está cadastrado."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpPut("v1/institutions/{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateInstitutionViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var institution = await context.Institutions.FirstOrDefaultAsync(x => x.Id == id);

                if (institution == null)
                    return NotFound(new ResultViewModel<string>("Instituição não encontrada"));

                institution.Name = model.Name;
                institution.TaxId = model.TaxId;
                institution.ContactEmail = model.ContactEmail;
                institution.Phone = model.Phone;
                institution.Address = model.Address;
                institution.IsActive = model.IsActive;

                context.Institutions.Update(institution);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Institution>(institution));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível atualizar a instituição."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/institutions/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var institution = await context.Institutions.FirstOrDefaultAsync(x => x.Id == id);

                if (institution == null)
                    return NotFound(new ResultViewModel<string>("Instituição não encontrada"));

                institution.IsActive = false;
                
                context.Institutions.Remove(institution);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<string>("Instituição inativada com sucesso."));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível excluir a instituição."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

    }
}