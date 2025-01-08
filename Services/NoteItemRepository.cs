using Microsoft.Data.SqlClient;
using note_taking_api.Models;
using Dapper;

namespace note_taking_api.Services
{
    public class NoteItemRepository : INoteItemRepository
    {
        private readonly IConfiguration _configuration;

        public NoteItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddAsync(NoteItem noteItem)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("INSERT INTO NoteItems (Description, Created, Updated) VALUES (@Description, @Created, @Updated)", noteItem);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            var sql = "DELETE FROM NoteItems WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<NoteItem>> GetAllAsync()
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM NoteItems";
            var noteItems = await connection.QueryAsync<NoteItem>(sql);
            return noteItems.ToList();
        }

        public async Task<NoteItem> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM NoteItems WHERE Id = @Id";
            var noteItem = await connection.QueryFirstOrDefaultAsync<NoteItem>(sql, new { Id = id });

            if (noteItem == null)
                throw new KeyNotFoundException($"NoteItem with Id {id} not found.");

            return noteItem;
        }

        public async Task UpdateAsync(NoteItem noteItem)
        {
            using var connection = GetConnection();
            var sql = "UPDATE NoteItems SET Description = @Description, Created = @Created, Updated = @Updated WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new
            {
                Id = noteItem.Id,
                Description = noteItem.Description,
                Created = noteItem.Created,
                Updated = noteItem.Updated
            });
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
