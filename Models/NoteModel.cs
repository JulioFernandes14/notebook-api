using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(400)]
        public string Description { get; set; } = String.Empty;

        [MaxLength(7)]
        public string HexColor { get; set; } = String.Empty;

        public UserModel User { get; set; } = null!;
    }
}
