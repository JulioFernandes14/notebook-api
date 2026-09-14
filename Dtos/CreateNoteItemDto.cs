using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Dtos
{
    public record CreateNoteItemDto
    (
        [Required]
        [MaxLength(255)]
        string Title,

        [Required]
        string Description
    );
}
