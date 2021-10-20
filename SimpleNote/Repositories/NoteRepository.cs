using Dapper;
using Microsoft.Extensions.Configuration;
using SimpleNote.Entities;
using SimpleNote.Entities.Persistence;
using SimpleNote.Infrastructures.SqlContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleNote.Repositories
{
    public class NoteRepository : SimpleNoteSqlContext, IPersistNote
    {
        public NoteRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<Note>> Get()
        {
            return await Db.QueryAsync<Note>(@"
                SELECT 
                    Id,
                    Text
                From Note
                ORDER BY CreatedDate
            ");
        }

        public async Task<Note> GetById(int id)
        {
            return await Db.QuerySingleAsync<Note>(@"
                SELECT 
                    Id,
                    Text
                From Note
                WHERE Id = @id
            ", new { id });
        }

        public async Task<Note> Insert(string text)
        {
            int id = await Db.ExecuteScalarAsync<int>(@"
                INSERT Note (Text, CreatedDate)
                OUTPUT INSERTED.Id
                Values (@text, @createdDate)
            ", new { text, createdDate = DateTime.Now });

            return new Note
            {
                Id = id,
                Text = text
            };
        }

        public async Task Update(Note note)
        {
            await Db.ExecuteAsync(@"
                UPDATE Note
                    Set Text = @text
                WHERE Id = @id
            ", new { id = note.Id, text = note.Text });
        }

        public async Task DeleteById(int id)
        {
            await Db.ExecuteAsync(@"
                DELETE FROM Note
                WHERE Id = @id
            ", new { id });
        }
    }
}
