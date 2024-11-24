using System.ComponentModel.DataAnnotations;

public class ConditionalEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;

        // If email is null or empty, it's considered valid
        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult.Success!;
        }

        // Use the EmailAddressAttribute to validate the email format
        var emailAddressAttribute = new EmailAddressAttribute();
        if (emailAddressAttribute.IsValid(email))
        {
            return ValidationResult.Success!;
        }

        // If email format is invalid, return a validation error
        return new ValidationResult(ErrorMessage ?? "The email format is invalid.");
    }
}