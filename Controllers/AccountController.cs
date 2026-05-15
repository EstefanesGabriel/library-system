using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.Enums;
using SistemaBiblioteca.Extension;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Services;
using SistemaBiblioteca.ViewModels;
using SistemaBiblioteca.ViewModels.Accounts;

namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] EmailService emailService,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            
            var password = PasswordGenerator.Generate(length: 25);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                NationalId = model.NationalId,
                InstitutionId = model.InstitutionId,
                PasswordHash = PasswordHasher.Hash(password),
                Role = UserRoleEnum.Reader,
                IsActive = true
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await emailService.Send(user.FullName, user.Email, "Bem vindo a biblioteca", $"Sua senha é {password}");

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<string>("Esse Email já está cadastrado!"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] DataContext context,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
            }
        }

        [Authorize]
        [HttpPost("v1/accounts/upload-image")]
        public async Task<IActionResult> UploadImage(
            [FromForm] UploadImageViewModel model,
            [FromServices] DataContext context
        )
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest(new ResultViewModel<string>("Nenhuma imagem foi enviada"));
            }

            var extension = Path.GetExtension(model.File.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            try
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha ao salvar a imagem no servidor."));
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.FullName == User.Identity.Name);

            if (user == null)
                return NotFound(new ResultViewModel<User>("Usuario não encontrado"));
            

            user.ProfilePictureUrl = $"https://localhost:0000/images/{fileName}";

            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }

            return Ok(new ResultViewModel<string>("Imagem alterada com sucesso!", null));
        }
        
    }
}