namespace UndoRedo.Providers.Commands
{
    public interface ICommandAdd : ICommand
    {
        void UpdateIdForNewItems();
    }
}
