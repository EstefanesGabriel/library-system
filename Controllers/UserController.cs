using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.Extension;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.ViewModels;
using SistemaBiblioteca.ViewModels.Users;

namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("v1/users")]
        public async Task<IActionResult> GetAsync(
            [FromServices] IMemoryCache cache,
            [FromServices] DataContext context
        )
        {
            try
            {
                var users = cache.GetOrCreate("UsersCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return context.Users.ToList();
                });
                //var users = await context.Users.ToListAsync();
                return Ok(new ResultViewModel<List<User>>(users));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<User>>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/users/{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteudo nao encontrado"));

                return Ok(new ResultViewModel<User>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/users")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateUserViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            try
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    NationalId = model.NationalId,
                    ProfilePictureUrl = model.ProfilePictureUrl,
                    Role = model.Role,
                    InstitutionId = model.InstitutionId,
                    IsActive = true
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"v1/users/{user.Id}", new ResultViewModel<User>(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Nao foi possivel incluir o usuario"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }

        }

        [HttpPut("v1/users/{id:Guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateUserViewModel model,
            [FromServices] DataContext context
        )
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteudo nao encontrado"));

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.NationalId = model.NationalId;
                user.ProfilePictureUrl = model.ProfilePictureUrl;
                user.Role = model.Role;
                user.IsActive = model.IsActive;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<User>(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Nao foi possivel atualizar o usuario"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/users/{id:Guid}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>("Conteudo nao encontrado"));

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<User>(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Nao foi possivel excluir o livro"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Falha interna no servidor"));
            }
        }

    }
}