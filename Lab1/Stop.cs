using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Stop
    {
        public string Name;
        public string Location;
        public DateTime OpenedFrom;
        public DateTime ClosedAt;
        

        public Stop(string name, string location, DateTime openedFrom, DateTime closedAt)
        {
            Name = name;
            Location = location;
            OpenedFrom = openedFrom;
            ClosedAt = closedAt;
        }
    }
}
