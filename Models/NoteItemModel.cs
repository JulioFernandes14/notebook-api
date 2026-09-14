using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Models
{
    public class NoteItemModel
    {
        public int Id { get; set; }

        [MaxLength(244)]
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public NoteModel Note { get; set; } = null!;
    }
}
