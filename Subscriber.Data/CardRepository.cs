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
    public class CardRepository : ICardRepository
    {
        private readonly WeightWatchers weightWatchers;
        private readonly IMapper mapper;
        private readonly ITrackingService trackingService;
        public CardRepository(WeightWatchers weightWatchers, IMapper mapper, ITrackingService trackingService)
        {
            this.weightWatchers = weightWatchers;
            this.mapper = mapper;
            this.trackingService = trackingService;
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
            trackingService.UpdateTable(new TrackingModel()
            {
                CardId = card.Id,
                Weight = card.Weight,
                BMI = card.BMI
            });
            weightWatchers.SaveChanges();
        }

        private double CalculateBMI(double height, double weight)
        {
            return weight / Math.Pow(height, 2);
        }
    }
}
