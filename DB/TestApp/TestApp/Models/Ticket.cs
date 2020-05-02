using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public User Buyer { get; set; }
        public int UserId { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public Seat Seat { get; set; }
        public int SeatId { get; set; }
    }
}