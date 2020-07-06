using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Models
{
    public class MUser
    {
        public MSubscriber Subscriber { get; set; }
        public MCard Card { get; set; }
    }
}
