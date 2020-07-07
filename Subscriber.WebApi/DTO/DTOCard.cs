using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Subscriber.WebApi.DTO
{
    public class DTOCard
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
    }
}
