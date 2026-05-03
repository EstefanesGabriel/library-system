using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "O e-mail é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O documento (CPF) é obrigatório!")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "A instituição é obrigatória")]
        public Guid InstitutionId { get; set; }
    }
}