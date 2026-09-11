using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Dtos
{
    public record UpdateNoteRequestDto
    (
        [MaxLength(100, ErrorMessage = "O campo Name deve conter no máximo 100 caracteres")]
        string? Name,

        [MaxLength(400, ErrorMessage = "O campo Description deve conter no máximo 400 caracteres")]
        string? Description,

        [RegularExpression(
            @"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$",
            ErrorMessage = "O campo HexColor deve ser um hexadecimal válido. Ex: #002AB2, #FFF"
        )]
        string? HexColor
    );
}
