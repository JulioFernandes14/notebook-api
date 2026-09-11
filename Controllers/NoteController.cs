using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotebookApi.Dtos;
using NotebookApi.Dtos.NotebookApi.Dtos;
using NotebookApi.Services;
using System.Security.Claims;

namespace NotebookApi.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public async Task<ActionResult<NoteResponseDto>> CreateNote(CreateNoteRequestDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var note = await _noteService.CreateNote(userId, dto);

            return Ok(note);
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteResponseDto>>> GetNotesByUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var notes = await _noteService.GetNotesByUser(userId);

            return Ok(notes);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<NoteResponseDto>> UpdateNote(int id, UpdateNoteRequestDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var note = await _noteService.UpdateNote(userId, id, dto);

            return Ok(note);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<NoteResponseDto>> UpdateNote(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            await _noteService.DeleteNote(userId, id);

            return Ok();
        }
    }
}
