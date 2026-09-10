using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Validations;

public class RequireSpecialCharacterAttribute : ValidationAttribute
{
    public RequireSpecialCharacterAttribute()
    {
        ErrorMessage = "A senha deve conter pelo menos um caractere especial.";
    }

    public override bool IsValid(object? value)
    {
        if (value is not string password)
            return false;

        return password.Any(c => !char.IsLetterOrDigit(c));
    }
}