using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Annotations
{
    public class RequireLowercaseAttribute : ValidationAttribute
    {
        public RequireLowercaseAttribute()
        {
            ErrorMessage = "A senha deve conter pelo menos uma letra minúscula.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not string password)
            {
                return false;
            }

            return password.Any(char.IsLower);
        }
    }
}
