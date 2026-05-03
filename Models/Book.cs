namespace SistemaBiblioteca.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string? CoverImageUrl { get; set; }
        public int PublicationYear { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}