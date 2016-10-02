using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UndoRedo.Models;

namespace UndoRedo.Providers.Commands
{
    public class DeleteAboutDataCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly CommandDto commandDto;

        public DeleteAboutDataCommand(ILoggerFactory loggerFactory, CommandDto commandDto)
        {
            this.logger = loggerFactory.CreateLogger(typeof(DeleteAboutDataCommand).Name);
            this.commandDto = commandDto;
        }

        public CommandDto ActualCommandDtoForNewState(CommandType type)
        {
            return this.commandDto;
        }

        public void UnExcute(DomainModelContext context)
        {
            var aboutData = this.commandDto.Payload.ToObject<AboutData>();
            var entity = context.AboutData.First(a => a.Id == aboutData.Id);
            entity.Deleted = false;
            this.logger.LogDebug("UnExcuted");
        }

        public void Excute(DomainModelContext context)
        {
            var aboutData = this.commandDto.Payload.ToObject<AboutData>();
            var entity = context.AboutData.First(a => a.Id == aboutData.Id);
            entity.Deleted = true;
            this.logger.LogDebug("Executed");
        }
    }
}
