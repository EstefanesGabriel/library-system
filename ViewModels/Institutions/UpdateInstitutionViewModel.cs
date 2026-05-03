using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Institutions
{
    public class UpdateInstitutionViewModel
    {
        [Required(ErrorMessage = "O nome da instituição é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O documento (CNPJ) é obrigatório")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail inserido é inválido")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "O telefone/celular é obrigatório")]
        public string Phone { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}