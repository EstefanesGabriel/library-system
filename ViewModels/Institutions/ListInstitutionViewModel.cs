namespace SistemaBiblioteca.ViewModels.Institutions
{
    public class ListInstitutionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string ContactEmail { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}