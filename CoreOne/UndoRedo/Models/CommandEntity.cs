using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UndoRedo.Models
{
    public class CommandEntity
    {
        [Key]
        public int Id { get; set; }

        public CommandType CommandType { get; set; }
        public PayloadType PayloadType { get; set; }
        public string Payload { get; set; }

        public string ActualClientRoute { get; set; }

        public static CommandEntity CreateCommandEntity(CommandDto commandDto)
        {
            var commandEntity = new CommandEntity
            {
                ActualClientRoute = commandDto.ActualClientRoute,
                CommandType = commandDto.CommandType,
                PayloadType = commandDto.PayloadType
            };
            if (commandDto.Payload != null)
            {
                commandEntity.Payload = commandDto.Payload.ToString();
            }
            return commandEntity;
        }

        public CommandDto ToCommandDto()
        {
            var commandDto = new CommandDto();

            commandDto.ActualClientRoute = ActualClientRoute;
            commandDto.CommandType = CommandType;
            commandDto.PayloadType = PayloadType;
            if (Payload != null)
            {
                commandDto.Payload = JObject.Parse(Payload);
            }

            return commandDto;
        }

    }
}
