using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services
{
    public class SubscriberServices : ISubscriberServices
    {
        private readonly ISubscriberRepository subscriberRepository;
        public SubscriberServices(ISubscriberRepository subscriberRepository)
        {
            this.subscriberRepository = subscriberRepository;
        }
        public int checkSubscriberEmail(string email,  string password)
        {
           return subscriberRepository.checkSubscriberEmail(email, password);
        }

        public bool NewSubscriber(MUser mUser)
        {
            return subscriberRepository.NewSubscriber(mUser);
        }
    }
}
