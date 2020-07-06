using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Subscriber.Data.Models
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
        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(ConfigurationManager.AppSettings.Get("WeightWatchers"));
            }
        }
       
    }
}

