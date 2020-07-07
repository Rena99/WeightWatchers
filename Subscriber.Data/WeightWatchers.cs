using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Subscriber.Data.Models;
using Subscriber.Data.Entities;

namespace Subscriber.Models
{
    public class WeightWatchers : DbContext
    {
        public WeightWatchers()

        {
        }
        public WeightWatchers(DbContextOptions<WeightWatchers> options)
          : base(options)
        {

        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Data.Models.Subscriber> Subscribers { get; set; }
        public DbSet<Measure> Measures { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=.\\sqlexpress; Database= WeightWatchers; Trusted_Connection = True;");
            }
        }
       
    }
}

