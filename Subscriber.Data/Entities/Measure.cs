using Subscriber.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Subscriber.Data.Entities
{
    public class Measure
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public virtual Card Card { get; set; }
    }
}
