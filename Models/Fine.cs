namespace SistemaBiblioteca.Models
{
    public class Fine
    {
        public Guid Id { get; set; }
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }
        public decimal Amount { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }
    }
}