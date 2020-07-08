using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Interfaces
{
    public interface ITrackingService
    {
        void UpdateTable(TrackingModel trackingModel);
        List<TrackingModel> GetTrackings(int id, int page, int size);
    }
}
