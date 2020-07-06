using AutoMapper;
using Subscriber.Data.Models;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;

namespace Subscriber.Data
{
    public class SubscriberRepository : ISubscriberRepository
    {
        public SubscriberRepository()
        {
        }
        private readonly WeightWatchers context;
        private readonly IMapper _mapper;
        public SubscriberRepository(IMapper mapper,WeightWatchers context)
        {
            _mapper = mapper;
            this.context = context;
        }
        public int checkSubscriberEmail(string email, string password)
        {
            Models.Subscriber subscriber = new Models.Subscriber();
            foreach (var item in context.Subscribers)
            {
                if (item.Email==email&&item.Password==password)
                {
                    subscriber = item;
                    break;
                }
            }
            foreach (var card in context.Cards)
            {
                if (card.SubscriberId == subscriber.Id)
                {
                    return card.Id;
                }
            }
            return 0;
        }

        public bool NewSubscriber(MUser mUser)
        {
            foreach (var item in context.Subscribers)
            {
                if (item.Email == mUser.Subscriber.Email)
                {
                    return false;
                }
            }
            try
            {
                Models.Subscriber subscriber = _mapper.Map<Models.Subscriber>(mUser.Subscriber);
                subscriber.Id = Guid.NewGuid();
                Card card = _mapper.Map<Card>(mUser.Card);
                card.OpenDate = DateTime.Now;
                card.UpdateDate = DateTime.Now;
                card.SubscriberId = subscriber.Id;
                context.Subscribers.Add(subscriber);
                context.Cards.Add(card);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
