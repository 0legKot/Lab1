using System;

namespace Lab1
{
    class Transport: AnyTransportClass
    {
        //public string Name { get; set; }
        public TransportType Type { get; set; }
        public int Capacity { get; set; }
        public decimal TicketPrice { get; set; }

        public Transport(string id, TransportType type, string capacity, string ticketPrice)
        {
            Name = id;
            Type = type ?? throw new Exception("No transport type selected");
            if (int.TryParse(capacity, out int tmpCapacity)) Capacity = tmpCapacity;
            else throw new Exception("Incorrect input for capacity");
            if (decimal.TryParse(ticketPrice, out decimal tmpPrice)) TicketPrice = tmpPrice;
            else throw new Exception("Incorrect input for ticket price");
        }
    }
}
