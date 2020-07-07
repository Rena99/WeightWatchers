using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Interfaces
{
    public interface ICardService
    {
        UserModel GetCard(int id);
        void UpdateCard(MeasureModel cardModel);
    }
}
