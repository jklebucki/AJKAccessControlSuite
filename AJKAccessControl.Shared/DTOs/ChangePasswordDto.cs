using System.ComponentModel.DataAnnotations;

namespace AJKAccessControl.Shared.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$",
        ErrorMessage = "Hasło musi zawierać co najmniej jedną wielką literę, jedną małą literę, jedną cyfrę oraz jeden znak niealfanumeryczny.")]
        public string Password { get; set; } = string.Empty;
    }
}