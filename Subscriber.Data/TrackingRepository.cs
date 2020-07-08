using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Models;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subscriber.Data
{
    public class TrackingRepository:ITrackingRepository
    {
        private readonly WeightWatchers weightWatchers;
        private readonly IMapper mapper;
        public TrackingRepository(WeightWatchers weightWatchers, IMapper mapper)
        {
            this.weightWatchers = weightWatchers;
            this.mapper = mapper;
        }

        public List<TrackingModel> GetTrackings(int id, int page, int size)
        {
            List<Tracking> trackings = (List<Tracking>)weightWatchers.Trackings.Where(t => t.CardId == id);
            List<TrackingModel> trackingModels = new List<TrackingModel>();
            trackings.Sort(new DateCompare());
            trackings.Reverse();
            int start = page * size;
            int end = start + size;
            for (int i = start; i < end && i < trackings.Count(); i++)
            {
                trackingModels.Add(mapper.Map<TrackingModel>(trackings[i]));
            }
            return trackingModels;
        }

        public void UpdateTable(TrackingModel trackingModel)
        {
            Tracking tracking = mapper.Map<Tracking>(trackingModel);
            tracking.Date = DateTime.Now;
            tracking.Trend = GetTrend(tracking.Weight);
            weightWatchers.Trackings.Add(tracking);
            weightWatchers.SaveChanges();
        }

        private bool GetTrend(double weight)
        {
            if (weightWatchers.Trackings.Count() == 0)
            {
                return true;
            }
            Tracking tracking = weightWatchers.Trackings.ToList().Last();
            if(tracking.Weight<weight)
            {
                return false;
            }
            return true;
        }
    }
    public class DateCompare : IComparer<Tracking>
    {
        public int Compare(Tracking x, Tracking y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
