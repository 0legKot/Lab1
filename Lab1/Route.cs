using System;
using System.Collections.Generic;

namespace Lab1
{

    class Route
    {
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public List<Stop> Stops { get; set; }
        public Transport Transport { get; set; }

        public Route(string name, string startLocation, string endLocation, List<Stop> stops, Transport transport)
        {
            Name = name;
            StartLocation = startLocation;
            EndLocation = endLocation;
            if (stops.Count > 1) Stops = stops; else throw new Exception("Not enough stops selected");
            Transport = transport ?? throw new Exception("No transport selected");
        }
    }
}
