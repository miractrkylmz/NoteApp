using DapperNoteAPI.DTOs;
using DapperNoteAPI.Models;
using DapperNoteAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperNoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>A list of notes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var note = await _noteService.GetByIdAsync(id);
            return Ok(note);
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <param name="dto">Note create data.</param>
        /// <returns>HTTP 200 if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateDto dto)
        {
            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedDate = DateTime.UtcNow,
            };
            await _noteService.CreateAsync(note);
            return Ok(note);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NoteUpdateDto dto)
        {
            var note = new Note
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content,
            };
            await _noteService.UpdateAsync(note);
            return Ok(note);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noteService.DeleteAsync(id);
            return Ok();
        }
    }
}
