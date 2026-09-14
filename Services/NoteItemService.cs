using Microsoft.EntityFrameworkCore;
using NotebookApi.Data;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Models;

namespace NotebookApi.Services
{
    public class NoteItemService
    {
        private readonly AppDbContext _context;
        private readonly NoteService _noteService;

        public NoteItemService(AppDbContext context, NoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        private async Task ValidateConflictTitle(int userId, int noteId, string title)
        {
            var noteItem = await _context.NoteItems.FirstOrDefaultAsync(noteItem => EF.Functions.ILike(noteItem.Title, title) && noteItem.Note.Id == noteId);

            if (noteItem is not null)
            {
                throw new ConflictException("Já existe um item com o mesmo título na nota atual");
            }
        }

        public async Task<NoteItemResponseDto> Create(int userId, int noteId, CreateNoteItemDto dto)
        {
            var note = await _noteService.GetNoteById(userId, noteId);

            await ValidateConflictTitle(userId, noteId, dto.Title);

            var noteItem = new NoteItemModel
            {
                Title = dto.Title,
                Description = dto.Description,
            };

            note.NoteItems.Add(noteItem);
            await _context.SaveChangesAsync();

            return new NoteItemResponseDto(noteItem.Id, noteItem.Title, noteItem.Description);
        }

        public async Task<NoteItemModel> GetNoteItemById(int userId, int noteItemId)
        {
            var noteItem = await _context.NoteItems
                .Include(noteItem => noteItem.Note)
                .ThenInclude(note => note.User)
                .FirstOrDefaultAsync(noteItem =>
                    noteItem.Id == noteItemId &&
                    noteItem.Note.User.Id == userId
                )
                ?? throw new NotFoundException(
                    "Item não encontrado para o usuário autenticado"
                );

            return noteItem;
        }

        public async Task<NoteItemResponseDto> Update(int userId, int noteItemId, UpdateNoteItemDto dto)
        {
            var noteItem = await GetNoteItemById(userId, noteItemId);

            if (dto.Title is not null)  
            {
                await ValidateConflictTitle(userId, noteItem.Note.Id, dto.Title);
                noteItem.Title = dto.Title;
            }

            if (dto.Description is not null)
            {
                noteItem.Description = dto.Description;
            }

            await _context.SaveChangesAsync();

            return new NoteItemResponseDto(noteItem.Id, noteItem.Title, noteItem.Description);
        }

        public async Task<NoteItemResponseDto> GetNoteItemDetailsById(int userId, int noteItemId)
        {
            var noteItem = await GetNoteItemById(userId, noteItemId);

            return new NoteItemResponseDto(noteItem.Id, noteItem.Title, noteItem.Description);
        }

        public async Task Delete(int userId, int noteItemId)
        {
            var noteItem = await GetNoteItemById(userId, noteItemId);

            _context.NoteItems.Remove(noteItem);
            await _context.SaveChangesAsync();
        }
    }
}
