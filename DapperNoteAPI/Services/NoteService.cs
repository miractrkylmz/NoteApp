using Dapper;
using DapperNoteAPI.Helpers;
using DapperNoteAPI.Models;
using System.Data;

namespace DapperNoteAPI.Services
{
    public class NoteService
    {
        private readonly DapperContext _context;

        public NoteService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            var notes = await connection.QueryAsync<Note>("sp_GetAllNotes",commandType:CommandType.StoredProcedure);
            return notes;
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            var query = "Select * from Notes Where Id=@Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Note>(query,new { Id =id });
        }

        public async Task CreateAsync(Note note)
        {
            using var connection = _context.CreateConnection();
            var parameters = new
            {
                Title = note.Title,
                Content = note.Content,
                CreatedDate = note.CreatedDate,
            };
            await connection.ExecuteAsync("sp_InsertNote",parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Note note)
        {
            using var connection = _context.CreateConnection();
            var parameters = new
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            };
            await connection.ExecuteAsync("sp_UpdateNote",parameters,commandType:CommandType.StoredProcedure);  
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var parameters = new
            {
               Id = id,
            };
            await connection.ExecuteAsync("sp_DeleteNote", parameters, commandType: CommandType.StoredProcedure);

        }
    }
}
