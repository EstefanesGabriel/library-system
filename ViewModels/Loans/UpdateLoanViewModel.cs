using System.ComponentModel.DataAnnotations;
using SistemaBiblioteca.Enums;

namespace SistemaBiblioteca.ViewModels.Loans
{
    public class UpdateLoanViewModel
    {
        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnedAt { get; set; }

        [Required(ErrorMessage = "O status do empréstimo é obrigatório")]
        public LoanStatusEnum LoanStatus { get; set; }
    }
}