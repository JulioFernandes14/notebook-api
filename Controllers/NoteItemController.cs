using Microsoft.AspNetCore.Mvc;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Services;
using System.Security.Claims;

namespace NotebookApi.Controllers
{

    [ApiController]
    [Route("/api/note-items")]
    public class NoteItemController : ControllerBase
    {
        private readonly NoteItemService _noteItemService;

        public NoteItemController(NoteItemService noteItemService)
        {
            _noteItemService = noteItemService;
        }

        [HttpPost]
        [Route("{noteId}")]
        public async Task<ActionResult<NoteItemResponseDto>> Create(int noteId, CreateNoteItemDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            try
            {
                var noteItem = await _noteItemService.Create(userId, noteId, dto);

                return Ok(noteItem);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<NoteItemResponseDto>> Update(int id, UpdateNoteItemDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            try
            {
                var noteItem = await _noteItemService.Update(userId, id, dto);

                return Ok(noteItem);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<NoteItemResponseDto>> GetNoteItemDetails(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            try
            {
                var noteItem = await _noteItemService.GetNoteItemDetailsById(userId, id);

                return Ok(noteItem);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            try
            {
                await _noteItemService.Delete(userId, id);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
