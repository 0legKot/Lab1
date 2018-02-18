using System;

namespace Lab1
{
    class TransportType:AnyTransportClass
    {
        //public string Name { get; set; }
        public Type Position { get; set; }
        public decimal AvgTicketPrice { get; set; }
        public int Quantity { get; set; }
        public int MonthServiceCost { get; set; }
        public int AccidentRate { get; set; }

        public TransportType(string name, Type position, string avgTicketPrice, string quantity, string monthServiceCost, string accidentRate)
        {
            Name = name;
            Position = position;
            if (decimal.TryParse(avgTicketPrice, out decimal tmpAvgPrice)) AvgTicketPrice = tmpAvgPrice;
            else throw new Exception("Incorrect input for average ticket price");
            if (int.TryParse(quantity, out int tmpQuantity)) Quantity = tmpQuantity;
            else throw new Exception("Incorrect input for quantity");
            if (int.TryParse(monthServiceCost, out int tmpCost)) MonthServiceCost = tmpCost;
            else throw new Exception("Incorrect input for month service cost");
            if (int.TryParse(accidentRate, out int tmpAccidentRate)) AccidentRate = tmpAccidentRate;
            else throw new Exception("Incorrect input for accident rate");
        }
    }
}
