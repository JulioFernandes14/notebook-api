using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace NotebookApi.Dtos
{
    public record UserResponseDto(
        int Id,
        string Name,
        string Email,
        string PhoneNumber
    );
}
