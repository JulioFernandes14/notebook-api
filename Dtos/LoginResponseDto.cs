namespace NotebookApi.Dtos
{
    public record LoginResponseDto
    (
        UserResponseDto User,
        string AccessToken
    );
}
