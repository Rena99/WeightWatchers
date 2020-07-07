using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IMeasureRepository measureRepository;
        public MeasureService(IMeasureRepository measureRepository)
        {
            this.measureRepository = measureRepository;
        }
        public int PostMeasure(MeasureModel measure)
        {
            return measureRepository.PostMeasure(measure);
        }

        public void UpdateStatus(int measureId)
        {
            measureRepository.UpdateStatus(measureId);
        }
    }
}
