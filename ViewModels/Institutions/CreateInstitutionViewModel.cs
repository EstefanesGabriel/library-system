using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Institutions
{
    public class CreateInstitutionViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O documento (CNPJ) é necessário")]
        public string TaxId { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "O telefone/celular é obrigatório")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O e-mail para contato é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
        public string ContactEmail { get; set; }
    }
}