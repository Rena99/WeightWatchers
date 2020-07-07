﻿using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Interfaces
{
    public interface IMeasureRepository
    {
        int PostMeasure(MeasureModel measure);
        void UpdateStatus(int measureId);
    }
}
