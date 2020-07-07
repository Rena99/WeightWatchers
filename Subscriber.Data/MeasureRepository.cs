using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Models;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;

namespace Subscriber.Data
{
    public class MeasureRepository : IMeasureRepository
    {
        private readonly WeightWatchers context;
        private readonly IMapper mapper;
        public MeasureRepository(WeightWatchers context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public int PostMeasure(MeasureModel measure)
        {
            Measure measure1 = mapper.Map<Measure>(measure);
            measure1.Date = DateTime.Now;
            context.Measures.Add(measure1);
            context.SaveChanges();
            return context.Measures.LastOrDefault().Id;
        }

        public void UpdateStatus(int measureId)
        {
            context.Measures.FirstOrDefault(m => m.Id == measureId).Status = "Success";
            context.SaveChanges();
        }
    }
}
