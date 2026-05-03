using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.ViewModels.Accounts
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage = "Imagem inválida")]
        public IFormFile File { get; set; }
    }
}