using note_taking_api.Models;

namespace note_taking_api.Services
{
    public interface INoteItemRepository
    {
        Task<List<NoteItem>> GetAllAsync();
        Task<NoteItem> GetByIdAsync(int id);
        Task AddAsync(NoteItem noteItem);
        Task UpdateAsync(NoteItem noteItem);
        Task DeleteAsync(int id);
    }
}
