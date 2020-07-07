using AutoMapper;
using Subscriber.Data.Models;
using Subscriber.Models;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Subscriber.Data
{
    public class SubscriberRepository : ISubscriberRepository
    {
        public SubscriberRepository()
        {
        }
        private readonly WeightWatchers context;
        private readonly IMapper _mapper;
        private readonly string salt = "E1F53135E559C253";
        public SubscriberRepository(IMapper mapper, WeightWatchers context)
        {
            _mapper = mapper;
            this.context = context;
        }
       
        public int checkSubscriberEmail(string email, string password)
        {
            Models.Subscriber subscriber = new Models.Subscriber();
            foreach (var item in context.Subscribers)
            {
                if (item.Email==email&&AreEqual(password, item.Password, salt))
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

        public bool NewSubscriber(UserModel mUser)
        {
            foreach (var item in context.Subscribers)
            {
                if (item.Email == mUser.Subscriber.Email)
                {
                    return false;
                }
            }
            Models.Subscriber subscriber = _mapper.Map<Models.Subscriber>(mUser.Subscriber);
            subscriber.Password = GenerateHash(subscriber.Password, salt);
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
        private string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }

       
    }
}
