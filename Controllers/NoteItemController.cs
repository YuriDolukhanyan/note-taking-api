using Microsoft.AspNetCore.Mvc;
using note_taking_api.Models;
using note_taking_api.Services;

namespace note_taking_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteItemController : ControllerBase
    {
        private readonly INoteItemRepository _noteItemRepository;

        public NoteItemController(INoteItemRepository noteItemRepository)
        {
            _noteItemRepository = noteItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteItem>>> GetAllAsync()
        {
            var noteItems = await _noteItemRepository.GetAllAsync();
            return Ok(noteItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteItem>> GetByIdAsync(int id)
        {
            try
            {
                var noteItem = await _noteItemRepository.GetByIdAsync(id);
                return Ok(noteItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"NoteItem with Id {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateNoteItemDto createNoteItemDto)
        {
            if (createNoteItemDto == null)
            {
                return BadRequest(new { Message = "NoteItem cannot be null." });
            }

            var noteItem = new NoteItem
            {
                Description = createNoteItemDto.Description,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _noteItemRepository.AddAsync(noteItem);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = noteItem.Id }, noteItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateNoteItemDto noteItem)
        {
            if (noteItem == null)
            {
                return BadRequest(new { Message = "NoteItem data cannot be null." });
            }

            try
            {
                var noteToUpdate = new NoteItem
                {
                    Id = id,
                    Description = noteItem.Description,
                    Created = noteItem.Created,
                    Updated = noteItem.Updated
                };

                await _noteItemRepository.UpdateAsync(noteToUpdate);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"NoteItem with Id {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _noteItemRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = $"NoteItem with Id {id} not found." });
            }
        }
    }
}
