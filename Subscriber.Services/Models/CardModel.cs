using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Models
{
    public class CardModel
    {
        public DateTime OpenDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
    }
}
