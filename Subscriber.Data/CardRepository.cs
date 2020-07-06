using AutoMapper;
using Subscriber.Data.Models;
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
        public MUser GetCard(int id)
        {
            try
            {
                Card card = weightWatchers.Cards.FirstOrDefault(c => c.Id == id);
                MCard mCard = mapper.Map<MCard>(card);
                MSubscriber subscriber = mapper.Map<MSubscriber>(weightWatchers.Subscribers.FirstOrDefault(s=>s.Id==card.SubscriberId));
                return new MUser()
                {
                    Card = mCard,
                    Subscriber = subscriber
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
