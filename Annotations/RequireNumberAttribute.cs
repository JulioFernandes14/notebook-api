using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Annotations
{
    public class RequireNumberAttribute : ValidationAttribute
    {
        public RequireNumberAttribute()
        {
            ErrorMessage = "A senha deve conter pelo menos um número.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not string password)
            {
                return false;
            }

            return password.Any(char.IsDigit);
        }
    }
}
