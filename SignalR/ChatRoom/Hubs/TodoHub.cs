using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatRoom.Hubs
{
    public class TodoHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TodoHub>();
            context.Clients.All.displayStatus();
        }
    }
}