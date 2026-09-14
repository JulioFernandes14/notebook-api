using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Dtos
{
    public record UpdateNoteItemDto
    (
        [MaxLength(255)]
        string? Title,

        string? Description
    );
}
