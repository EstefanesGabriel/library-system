using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Loans
{
    public class CreateLoanViewModel
    {
        [Required(ErrorMessage = "O livro é obrigatório")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A instituição é obrigatória")]
        public Guid InstitutionId { get; set; }
    }
}