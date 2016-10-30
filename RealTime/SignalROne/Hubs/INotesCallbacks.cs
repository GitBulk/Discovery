using SignalROne.Models.Notes;
using System.Threading.Tasks;

namespace SignalROne.Hubs
{
    // Client callbacks
    public interface INotesCallbacks
    {
        // Notify note added
        Task BroadcastNewNote(Note newNote);
        // Notify note removed
        Task BroadcastRemoveNote(int noteId);
    }
}
