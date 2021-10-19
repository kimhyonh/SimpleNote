using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleNote.Entities.Persistence
{
    public interface IPersistNote
    {
        Task<IEnumerable<Note>> Get();
        Task<Note> GetById(int id);
        Task<Note> Insert(string text);
        Task Update(Note note);
        Task DeleteById(int id);
    }
}
