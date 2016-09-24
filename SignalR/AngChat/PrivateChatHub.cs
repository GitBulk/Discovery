using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AngChat
{
    public class PrivateChatHub : Hub
    {
        public void Send(string username, string message, string privateKey)
        {
            Clients.Client(privateKey).sendMessageToClient(username, message);
        }
    }
}