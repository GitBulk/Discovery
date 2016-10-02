using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UndoRedo.Models;

namespace UndoRedo.Providers.Commands
{
    public class AddAboutDataCommand : ICommandAdd
    {
        private readonly ILogger logger;
        private readonly CommandDto commandDto;
        private AboutData aboutData;

        public AddAboutDataCommand(ILoggerFactory loggerFactory, CommandDto commandDto)
        {
            this.logger = loggerFactory.CreateLogger(typeof(AddAboutDataCommand).Name);
            this.commandDto = commandDto;
        }

        public CommandDto ActualCommandDtoForNewState(CommandType type)
        {
            throw new NotImplementedException();
        }

        public void Excute(DomainModelContext context)
        {
            this.aboutData = this.commandDto.Payload.ToObject<AboutData>();
            if (this.aboutData.Id > 0)
            {
                this.aboutData.Deleted = false;
                context.AboutData.Update(aboutData);
            }
            else
            {
                context.AboutData.Add(this.aboutData);
            }
            this.logger.LogDebug("Executed");
        }

        public void UnExcute(DomainModelContext context)
        {
            this.aboutData = this.commandDto.Payload.ToObject<AboutData>();
            this.aboutData.Deleted = true;
            var entity = context.AboutData.First(t => t.Id == aboutData.Id);
            entity.Deleted = true;
            this.logger.LogDebug("UnExecuted");
        }

        public void UpdateIdForNewItems()
        {
            this.commandDto.Payload = JObject.FromObject(this.aboutData);
        }
    }
}
