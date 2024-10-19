using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AJKAccessControl.Shared.DTOs
{
    public class RegisterUserDto
    {
        private string _userName = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa użytkownika musi mieć co najmniej 2 znaki.")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Nazwa użytkownika może zawierać wyłącznie duże litery.")]
        public string UserName
        {
            get => _userName;
            set => _userName = value.ToUpper();
        }
        
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email."), AllowNull]
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$",
        ErrorMessage = "Hasło musi zawierać co najmniej jedną wielką literę, jedną małą literę, jedną cyfrę oraz jeden znak niealfanumeryczny.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; } = string.Empty;
    }
}