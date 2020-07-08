using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Models
{
    public class TrackingModel
    {
        public int CardId { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
        public bool Trend { get; set; }
        public double BMI { get; set; }
        public string Comments { get; set; }
    }
}
