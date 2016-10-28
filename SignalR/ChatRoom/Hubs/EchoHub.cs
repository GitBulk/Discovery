using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChatRoom.Hubs
{
    [HubName("echo")]
    public class EchoHub : Hub
    {
        private static int Calls = 0;
        private static readonly HashSet<string> ConnectionIds = new HashSet<string>();

        public void Hello()
        {
            string msg = string.Format("Greetings {0}, It's {1:F} and I am a robot", Context.ConnectionId, DateTime.Now);
            var caller = Clients.Caller;
            caller.greeting(msg);
        }

        public void HelloAll()
        {
            string msg = string.Format("Hello all {0}, It's {1:F} and I am a robot", Context.ConnectionId, DateTime.Now);
            var all = Clients.All;
            all.greeting(msg);
        }

        public void Subscribe(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void UnSubscribe(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }

        public void HelloGroup(string groupName)
        {
            var msg = string.Format("Welcome from {0}", groupName);
            Clients.Group(groupName).greeting(msg);
        }

        public void HelloOthers()
        {
            string msg = string.Format("Greetings {0}, It's {1:F} and I am a robot", Context.ConnectionId, DateTime.Now);
            var others = Clients.Others;
            others.greeting(msg);
        }

        public void JoinRoom()
        {
            var conId = Context.ConnectionId;
            ConnectionIds.Add(conId);
            Clients.All.connections(ConnectionIds);
        }

        public void HelloBut(string excludeConnectionId)
        {
            string msg = string.Format("Greetings {0}, It's {1:F} and I am a robot", Context.ConnectionId, DateTime.Now);
            var allExcept = Clients.AllExcept(excludeConnectionId);
            allExcept.greeting(msg);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var conId = Context.ConnectionId;
            ConnectionIds.Remove(conId);
            return base.OnDisconnected(stopCalled);
        }
    }
}