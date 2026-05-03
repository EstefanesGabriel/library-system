namespace SistemaBiblioteca.ViewModels.Books
{
    public class ListBookViewModel
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
    }
}