using SistemaBiblioteca.Enums;

namespace SistemaBiblioteca.Models
{
    public class Loan
    {
        public Guid Id { get; set; } 
        public Guid BookId { get; set; } 
        public Book Book { get; set; }
        public Guid UserId { get; set; } 
        public User User { get; set; } 
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public LoanStatusEnum LoanStatus { get; set; }
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public ICollection<Fine> Fines { get; set; }
    }
}