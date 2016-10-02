using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UndoRedo.Models;

namespace UndoRedo.Providers.Commands
{
    public interface ICommand
    {
        CommandDto ActualCommandDtoForNewState(CommandType type);
        void UnExcute(DomainModelContext context);
        void Excute(DomainModelContext context);
    }
}
