using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{

    class Route
    {
        public string Name;
        public string StartLocation;
        public string EndLocation;
        public List<Stop> Stops;
        public Transport Transport;

        public Route(string name, string startLocation, string endLocation, List<Stop> stops, Transport transport)
        {
            Name = name;
            StartLocation = startLocation;
            EndLocation = endLocation;
            Stops = stops;
            Transport = transport;
        }
    }
}
