using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public class EventContract
    {
        public string Id { get; set; }

        public int date { get; set; }

        public string description { get; set; }
    }
}
