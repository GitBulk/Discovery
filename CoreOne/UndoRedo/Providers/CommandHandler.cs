using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UndoRedo.Models;
using UndoRedo.Providers.Commands;

namespace UndoRedo.Providers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ICommandDataAccessProvider dataAccessProvider;
        private readonly DomainModelContext context;
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger logger;

        private static ConcurrentStack<ICommand> UndoCommands = new ConcurrentStack<ICommand>();
        private static ConcurrentStack<ICommand> RedoCommands = new ConcurrentStack<ICommand>();


        public CommandHandler(ICommandDataAccessProvider dataAccessProvider, DomainModelContext context, ILoggerFactory loggerFactory)
        {
            this.dataAccessProvider = dataAccessProvider;
            this.context = context;
            this.loggerFactory = loggerFactory;
            this.logger = this.loggerFactory.CreateLogger(typeof(CommandHandler).Name);
        }

        public void Excute(CommandDto commandDto)
        {
            if (commandDto.PayloadType == PayloadType.About)
            {
                ExcuteAboutDataCommand(commandDto);
            }
            else if (commandDto.PayloadType == PayloadType.Home)
            {
                ExcuteHomeDataCommand(commandDto);
            }
            else
            {
                ExcuteNoDataCommand(commandDto);
            }
        }

        private void ExcuteAboutDataCommand(CommandDto commandDto)
        {
            if (commandDto.CommandType == CommandType.Add)
            {
                ICommandAdd command = new AddAboutDataCommand(this.loggerFactory, commandDto);
                command.Excute(this.context);
                this.dataAccessProvider.AddCommand(CommandEntity.CreateCommandEntity(commandDto));
                this.dataAccessProvider.Save();
                command.UpdateIdForNewItems();
                UndoCommands.Push(command);
            }
            else if (commandDto.CommandType == CommandType.Update)
            {
                ICommandAdd command = new UpdateAboutDataCommand(this.loggerFactory, commandDto);
                command.Excute(this.context);
                this.dataAccessProvider.AddCommand(CommandEntity.CreateCommandEntity(commandDto));
                this.dataAccessProvider.Save();
                UndoCommands.Push(command);
            }
            else if (commandDto.CommandType == CommandType.Delete)
            {
                ICommandAdd command = new DeleteAboutDataCommand(this.loggerFactory, commandDto);
                command.Excute(this.context);
                this.dataAccessProvider.AddCommand(CommandEntity.CreateCommandEntity(commandDto));
                this.dataAccessProvider.Save();
                UndoCommands.Push(command);
            }
        }

        private void ExcuteHomeDataCommand(CommandDto commandDto)
        {
            throw new NotImplementedException();
        }

        private void ExcuteNoDataCommand(CommandDto commandDto)
        {
            this.dataAccessProvider.AddCommand(CommandEntity.CreateCommandEntity(commandDto));
            this.dataAccessProvider.Save();
        }

        public CommandDto Redo()
        {
            throw new NotImplementedException();
        }

        public CommandDto Undo()
        {
            var commandDto = new CommandDto
            {
                CommandType = CommandType.Undo,
                PayloadType = PayloadType.None,
                ActualClientRoute = PayloadType.None.ToString()
            };
            if (UndoCommands.Count > 0)
            {
                ICommand command;
                if (UndoCommands.TryPop(out command))
                {
                    RedoCommands.Push(command);
                    command.UnExcute(this.context);
                    commandDto.Payload = commandDto
                }
            }
        }
    }
}
