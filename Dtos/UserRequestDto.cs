using NotebookApi.Annotations;
using NotebookApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Dtos
{
    public record UserRequestDto(
        [Required(ErrorMessage = "Campo Name é obrigatório")]
        [MaxLength(140, ErrorMessage = "Campo Name deve ter no máximo 140 caracteres")]
        string Name,

        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo Email deve conter um endereço de email válido")]
        [MaxLength(140, ErrorMessage = "Campo Name deve ter no máximo 140 caracteres")]
        string Email,

        [Required(ErrorMessage = "Campo PhoneNumber é obrigatório")]
        [StringLength(13, MinimumLength = 13,ErrorMessage = "O número de telefone deve ter exatamente 13 caracteres, formato de ex: 5521954281835")]
        string PhoneNumber,

        [Required(ErrorMessage = "Campo Password é obrigatório")]
        [RequireSpecialCharacter(ErrorMessage = "Campo Password deve conter um caractér especial")]
        [RequireLowercase(ErrorMessage = "Campo Password deve conter uma letra minúscula")]
        [RequireNumber(ErrorMessage = "Campo Password deve conter um número")]
        [RequireUppercase(ErrorMessage = "Campo Password deve conter uma letra maiúscula")]
        [MinLength(8, ErrorMessage = "Campo Password deve ter no mínimo 8 caracteres")]
        [MaxLength(100, ErrorMessage = "Campo Password deve ter no máximo 100 caracteres")]
        string Password
    );
}
