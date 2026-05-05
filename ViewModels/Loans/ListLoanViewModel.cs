using SistemaBiblioteca.Enums;

namespace SistemaBiblioteca.ViewModels.Loans
{
    public class ListLoanViewModel
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string LoanStatus { get; set; }
    }
}