using NotebookApi.Annotations;
using NotebookApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Dtos
{
    public record LoginRequestDto
    (
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo Email deve conter um endereço de email válido")]
        string Email,

        [Required(ErrorMessage = "Campo Password é obrigatório")]
        string Password
    );
}
