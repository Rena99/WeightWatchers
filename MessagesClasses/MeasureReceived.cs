using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesClasses
{
    public class MeasureReceived:IEvent
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Weight { get; set; }
    }
}
