namespace NotebookApi.Dtos
{
    public record NoteWithItemsResponseDto
    (
        int Id,
        string Name,
        string Description,
        string HexColor,
        List<NoteItemSummaryResponseDto> NoteItems
    );
}
