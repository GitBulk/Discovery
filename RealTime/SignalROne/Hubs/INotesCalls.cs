using SignalROne.Models.Notes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalROne.Hubs
{
    // Client calls
    public interface INotesCalls
    {
        // Add note
        Task AddNote(string note);
        // Get all notes
        IEnumerable<Note> GetAllNotes();
        // Remove note
        Task RemoveNote(int roomId);
    }
}
