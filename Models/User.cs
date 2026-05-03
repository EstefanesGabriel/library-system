using System.Text.Json.Serialization;
using SistemaBiblioteca.Enums;

namespace SistemaBiblioteca.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } 
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string NationalId { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public UserRoleEnum Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}