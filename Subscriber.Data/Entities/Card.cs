using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Subscriber.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public double BMI { get; set; } = 0;
        public double Height { get; set; }
        public double Weight { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid SubscriberId { get; set; }
        public virtual Subscriber Subscriber { get; set; }

    }
}
