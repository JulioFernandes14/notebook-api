using Microsoft.EntityFrameworkCore;
using NotebookApi.Data;
using NotebookApi.Dtos;
using NotebookApi.Dtos.NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Models;

namespace NotebookApi.Services
{
    public class NoteService
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;

        public NoteService(AppDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        private async Task ValidateConflictName(string name)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(note => EF.Functions.ILike(note.Name, name));

            if (note is not null)
            {
                throw new ConflictException("O usuário ja possui uma anotação com o mesmo nome");
            }
        }

        public async Task<NoteModel> GetNoteById(int userId, int noteId)
        {
            var note = _context.Notes.FirstOrDefault(note => note.Id == noteId && note.User.Id == userId) ?? throw new NotFoundException("Nota não encontrada para o usuário autenticado");

            return note;
        }

        public async Task<NoteResponseDto> CreateNote(int userId, CreateNoteRequestDto dto)
        {
            var user = await _userService.GetUserModelById(userId);

            await ValidateConflictName(dto.Name);

            var note = new NoteModel
            {
                Name = dto.Name,
                Description = dto.Description,
                HexColor = dto.HexColor,
                User = user
            };

            await _context.AddAsync(note);
            await _context.SaveChangesAsync();

            return new NoteResponseDto
            (
                note.Id,
                note.Name,
                note.Description,
                note.HexColor
            );
        }

        public async Task<List<NoteResponseDto>> GetNotesByUser(int userId)
        {
            return await _context.Notes
                .Where(note => note.User.Id == userId)
                .Select(note => new NoteResponseDto
                (
                    note.Id,
                    note.Name,
                    note.Description,
                    note.HexColor
                ))
                .ToListAsync();
        }

        public async Task<NoteWithItemsResponseDto> GetNoteDetails(int userId, int noteId)
        {
            var note = await _context.Notes
                .Where(note => note.Id == noteId && note.User.Id == userId)
                .Select(note => new NoteWithItemsResponseDto
                (
                    note.Id,
                    note.Name,
                    note.Description,
                    note.HexColor,
                    note.NoteItems.Select(noteItem => new NoteItemSummaryResponseDto(noteItem.Id, noteItem.Title)).ToList()
                )).FirstOrDefaultAsync() ?? throw new NotFoundException("Nota não encontrada para o usuário autenticado");

            return note;
        }

        public async Task<NoteResponseDto> UpdateNote(int userId, int noteId, UpdateNoteRequestDto dto)
        {
            await _userService.GetUserModelById(userId);
            var note = await GetNoteById(userId, noteId);

            if (dto.Name is not null)
            {
                await ValidateConflictName(dto.Name);
                note.Name = dto.Name;
            }

           if (dto.Description is not null)
           {
                note.Description = dto.Description;
           }

            if (dto.HexColor is not null)
            {
                note.HexColor = dto.HexColor;
            }

            await _context.SaveChangesAsync();

            return new NoteResponseDto
            (
                note.Id,
                note.Name,
                note.Description,
                note.HexColor
            );
        }

        public async Task DeleteNote(int userId, int noteId) 
        {
            var note = await GetNoteById(userId, noteId);

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
