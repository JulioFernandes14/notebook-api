using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotebookApi.Models
{
    [Index(nameof (Email), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class UserModel
    {   
        public int Id { get; set; }

        [MaxLength(140)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(140)]
        public string Email { get; set; } = String.Empty;

        [MaxLength(30)]
        public string PhoneNumber { get; set; } = String.Empty;

        [MaxLength(100)]
        public string Password {  get; set; } = String.Empty;
    }
}
