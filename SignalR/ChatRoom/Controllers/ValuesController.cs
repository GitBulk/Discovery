using ChatRoom.Models;
using ChatRoom.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatRoom.Controllers
{
    public class ValuesController : ApiController
    {
        TodoRepository todoRepo = new TodoRepository();

        public IEnumerable<Todo> Get()
        {
            return todoRepo.GetData();
        }
    }
}
