using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesClasses
{
    public class UpdateMeasureStatus:ICommand
    {
        public int Id { get; set; }
    }
}
