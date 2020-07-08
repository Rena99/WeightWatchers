using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Subscriber.WebApi.DTO
{
    public class DTOTracking
    {
        public double Weight { get; set; }
        public DateTime Date { get; set; }
        public bool Trend { get; set; }
        public double BMI { get; set; }
        public string Comments { get; set; }
    }
}
