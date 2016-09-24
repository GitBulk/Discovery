using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PlanSeats.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace PlanSeats
{
    [HubName("SeatHub")]
    public class SeatHub : Hub
    {
        private static int UserId;
        private static List<Seat> AllSeats = new List<Seat>();

        public void CreateUser()
        {
            // increments the userId every time a browser opens up.
            UserId++;
            Clients.All.createUser(UserId);
        }

        public void PopulateSeatData()
        {
            // Whenever a client navigates to index.html, it will be passing the seats data to the client.
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(AllSeats);
            Clients.All.populateSeatData(result);
        }

        public void SelectSeat(int userId, int seatNumber)
        {
            // Whenever a client selects a seat, it will be pushing the seat data to all of the clients.
            var seat = new Seat()
            {
                SeatNumber = seatNumber,
                UserId = userId
            };
            AllSeats.Add(seat);
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(seat);
            Clients.All.selectSeat(result);
        }

    }
}