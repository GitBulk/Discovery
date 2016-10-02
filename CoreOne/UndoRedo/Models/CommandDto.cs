using Newtonsoft.Json.Linq;

namespace UndoRedo.Models
{
    public class CommandDto
    {
        public CommandType CommandType { get; set; }
        public PayloadType PayloadType { get; set; }
        public JObject Payload { get; set; }
        public string ActualClientRoute { get; set; }


    }
}
