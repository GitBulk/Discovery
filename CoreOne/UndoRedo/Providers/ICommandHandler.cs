using UndoRedo.Models;

namespace UndoRedo.Providers
{
    public interface ICommandHandler
    {
        CommandDto Undo();
        CommandDto Redo();
        void Excute(CommandDto value);
    }
}