using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Books
{
    public class UpdateBookViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório")]
        public string Author { get; set; }

        [Required(ErrorMessage = "O ISBN é obrigatório")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório")]
        public string Genre { get; set; }

        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        public int PublicationYear { get; set; } 

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "O status de atividade é obrigatório")]
        public bool IsActive { get; set; }
    }
}