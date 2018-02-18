using System;

namespace Lab1
{
    class Stop
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime OpenedFrom { get; set; }
        public DateTime ClosedAt { get; set; }


        public Stop(string name, string location, string openedFrom, string closedAt)
        {
            Name = name;
            Location = location;
            if (!DateTime.TryParse(openedFrom, out DateTime tmpOpenedFrom))
                throw new Exception("Incorrect input for opened from");
            if (!DateTime.TryParse(closedAt, out DateTime tmpClosedAt))
                throw new Exception("Incorrect input for closed at");
            if (tmpOpenedFrom < tmpClosedAt)
            {
                OpenedFrom = tmpOpenedFrom;
                ClosedAt = tmpClosedAt;
            }
            else throw new Exception("Closing must be after opening");
        }
    }
}
