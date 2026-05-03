namespace SistemaBiblioteca.Models
{
    public class Institution
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Fine> Fines { get; set; }
    }
}