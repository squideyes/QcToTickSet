using System;

namespace QcToTickSet
{
    public class Tick
    {
        public Symbol Symbol { get; set; }
        public DateTime TickOn { get; set; }
        public double BidRate { get; set; }
        public double AskRate { get; set; }
    }
}
