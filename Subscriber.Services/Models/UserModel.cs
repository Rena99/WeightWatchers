using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Models
{
    public class UserModel
    {
        public SubscriberModel Subscriber { get; set; }
        public CardModel Card { get; set; }
    }
}
