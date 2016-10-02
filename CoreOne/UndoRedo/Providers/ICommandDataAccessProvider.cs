using UndoRedo.Models;

namespace UndoRedo.Providers
{
    public interface ICommandDataAccessProvider
    {
        void AddCommand(CommandEntity commandEntity);
        void Save();
    }
}