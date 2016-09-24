using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using ChatRoom.Extensions;

namespace ChatRoom
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        static readonly HashSet<string> Rooms = new HashSet<string>();
        static List<User> LoggedInUsers = new List<User>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public string Login(string name)
        {
            string connectionId = Context.ConnectionId;
            bool result = LoggedInUsers.Any(x => x.Name == name && x.ConnectionId == connectionId);

            if (!result)
            {
                var user = new User
                {
                    Name = name,
                    ConnectionId = connectionId,
                    Age = 20,
                    Avatar = "",
                    Id = 1,
                    Sex = "Male",
                    MemberType = "registered",
                    FontColor = "Red",
                    Status = Staus.Online.ToString()
                };
                Clients.Caller.rooms(Rooms.ToArray());
                Clients.Caller.setInitial(Context.ConnectionId, name);
                //string jsonUsers = JsonConvert.SerializeObject(LoggedInUsers);
                string jsonUsers = LoggedInUsers.ToCamelCaseJson();
                LoggedInUsers.Add(user);
                Clients.Caller.getOnlineUsers(jsonUsers);
                Clients.Others.newOnlineUser(user);
            }

            return name;
        }

        public void SendPrivateMessage(string toConnectionId, string message)
        {
            string fromConnectionId = Context.ConnectionId;
            var toUser = LoggedInUsers.FirstOrDefault(x => x.ConnectionId == toConnectionId);
            var fromUser = LoggedInUsers.FirstOrDefault(x => x.ConnectionId == fromConnectionId);
            if (toUser != null && fromUser != null)
            {
                Clients.Client(toConnectionId).recievingPrivateMessage(fromConnectionId, fromUser.Name, message);
                Clients.Caller.recievingPrivateMessage(toConnectionId, fromUser.Name, message);
            }
        }

        public void UpdateStatus(string status)
        {
            string connectionId = Context.ConnectionId;
            LoggedInUsers.FirstOrDefault(x => x.ConnectionId == connectionId).Status = status;
            Clients.Others.statusChanged(connectionId, status);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User item = LoggedInUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                LoggedInUsers.Remove(item);
                var id = Context.ConnectionId;
                Clients.Others.newOfflineUser(item);
            }
            return base.OnDisconnected(stopCalled);
        }

        public void Connect(string userName)
        {
            string connectionId = Context.ConnectionId;
            if (LoggedInUsers.Count(x => x.ConnectionId == connectionId) == 0)
            {
                LoggedInUsers.Add(new User
                {
                    ConnectionId = connectionId,
                    Name = userName
                });
                Clients.Caller.onConnected(connectionId, userName, LoggedInUsers);
                Clients.AllExcept(connectionId).onNewUserConnected(connectionId, userName);
            }
        }

        public void UserTyping(string anotherConnectionId, string message)
        {
            string connectionId = Context.ConnectionId;
            Clients.Client(anotherConnectionId).isTyping(connectionId, message);
        }
    }

    enum Staus
    {
        Online,
        Away,
        Busy,
        Offline
    }

    class User
    {
        public string ConnectionId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> FriendList { get; set; }
        public string FontName { get; set; }
        public string FontSize { get; set; }
        public string FontColor { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }
        public string MemberType { get; set; }
        public string Avatar { get; set; }
    }
}