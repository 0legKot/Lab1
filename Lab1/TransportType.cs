using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    enum Type : byte
    {
        Air,
        Water,
        Land,
        Underground,
        Other
    }
    class TransportType
    {
        public string Name;
        public Type Position;
        public double AvgTicketPrice;
        public int Quantity;
        public int MonthServiceCost;
        public int AccidentRate;

        public TransportType(string name, Type position, double avgTicketPrice, int quantity, int monthServiceCost, int accidentRate)
        {
            Name = name;
            Position = position;
            AvgTicketPrice = avgTicketPrice;
            Quantity = quantity;
            MonthServiceCost = monthServiceCost;
            AccidentRate = accidentRate;
        }
    }
}
