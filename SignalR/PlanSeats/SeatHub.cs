using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PlanSeats.Models;

namespace PlanSeats
{
    public class SeatHub : Hub
    {
        private static int UserId;
        private static List<Seat> AllSeats = new List<Seat>();

        public void CreateUser()
        {
            UserId++;
            Clients.All.createUser(UserId);
        }

        public void PopulateSeatData()
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(AllSeats);
            Clients.All.populateSeatData(result);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}