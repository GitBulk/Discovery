using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UndoRedo.Models;
using UndoRedo.Providers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UndoRedo.Controllers
{
    [Route("api/[controller]")]
    public class CommandController : Controller
    {
        private readonly ICommandHandler commandHandler;
        public CommandController(ICommandHandler commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpPost]
        [Route("Undo")]
        public IActionResult Undo()
        {
            var commandDto = this.commandHandler.Undo();
            return Ok(commandDto);
        }

        public IActionResult Redo()
        {
            var commandDto = this.commandHandler.Redo();
            return Ok(commandDto);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CommandDto value)
        {
            this.commandHandler.Excute(value);
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
