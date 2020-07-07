using AutoMapper;
using Subscriber.Data.Models;
using Subscriber.Models;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subscriber.Data
{
    public class CardRepository : ICardRepository
    {
        private readonly WeightWatchers weightWatchers;
        private readonly IMapper mapper;
        public CardRepository(WeightWatchers weightWatchers, IMapper mapper)
        {
            this.weightWatchers = weightWatchers;
            this.mapper = mapper;
        }
        public UserModel GetCard(int id)
        {
            Card card = weightWatchers.Cards.FirstOrDefault(c => c.Id == id);
            CardModel mCard = mapper.Map<CardModel>(card);
            SubscriberModel subscriber = mapper.Map<SubscriberModel>(weightWatchers.Subscribers.FirstOrDefault(s => s.Id == card.SubscriberId));
            return new UserModel()
            {
                Card = mCard,
                Subscriber = subscriber
            };
        }
        public void UpdateCard(MeasureModel cardModel)
        {
            Card card = weightWatchers.Cards.FirstOrDefault(c => c.Id == cardModel.CardId);
            card.UpdateDate = DateTime.Now;
            card.Weight = (double)cardModel.Weight;
            card.BMI = CalculateBMI(card.Height, card.Weight);
            weightWatchers.SaveChanges();
        }

        private double CalculateBMI(double height, double weight)
        {
            return weight / Math.Pow(height, 2);
        }
    }
}
