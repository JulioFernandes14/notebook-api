using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NotebookApi.Dtos
{
    public record CreateNoteRequestDto
    (
        [Required(ErrorMessage = "Campo Name é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo Name deve conter no máximo 100 caracteres")]
        string Name,

        [Required(ErrorMessage = "Campo Name é obrigatório")]
        [MaxLength(400, ErrorMessage = "O campo Description deve conter no máximo 400 caracteres")]
        string Description,

        [Required(ErrorMessage = "Campo Name é obrigatório")]
        [RegularExpression(
            @"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$",
            ErrorMessage = "O campo HexColor deve ser um hexadecimal válido. Ex: #002AB2, #FFF"
        )]
        string HexColor
    );
}
