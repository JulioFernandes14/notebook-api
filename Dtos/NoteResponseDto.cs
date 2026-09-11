namespace NotebookApi.Dtos
{
    using System.ComponentModel.DataAnnotations;

    namespace NotebookApi.Dtos
    {
        public record NoteResponseDto
        (
            int Id,
            string Name,
            string Description,
            string HexColor
        );
    }

}
