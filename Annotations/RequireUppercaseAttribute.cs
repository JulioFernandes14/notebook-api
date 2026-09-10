using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Annotations
{
    public class RequireUppercaseAttribute : ValidationAttribute
    {
        public RequireUppercaseAttribute()
        {
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not string password)
                return false;

            return password.Any(char.IsUpper);
        }
    }
}
