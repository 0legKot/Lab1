using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Transport
    {
        public string Id;
        public TransportType Type;
        public int Capacity;
        public int TicketPrice;

        public Transport(string id, TransportType type, int capacity, int ticketPrice)
        {
            Id = id;
            Type = type;
            Capacity = capacity;
            TicketPrice = ticketPrice;
        }
    }
}
