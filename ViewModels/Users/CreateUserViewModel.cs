using System.ComponentModel.DataAnnotations;
using SistemaBiblioteca.Enums;

namespace SistemaBiblioteca.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
        [Required(ErrorMessage = "O documento (CPF) é obrigatório")]
        public string NationalId { get; set; }
        public string? ProfilePictureUrl { get; set; }
        [Required(ErrorMessage = "O nível de acesso é obrigatório")]
        public UserRoleEnum Role { get; set; }
        [Required(ErrorMessage = "A instituição é obrigatória")]
        public Guid InstitutionId { get; set; }
    }
}