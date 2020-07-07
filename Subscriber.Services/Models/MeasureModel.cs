using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Models
{
    public class MeasureModel
    {
        public int CardId { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
