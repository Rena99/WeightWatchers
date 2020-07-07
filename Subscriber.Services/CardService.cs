using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.Services.Interfaces
{
    public class CardService : ICardService
    {
        private readonly ICardRepository cardRepository;

        public CardService()
        {
        }

        public CardService(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }
        public UserModel GetCard(int id)
        {
            return cardRepository.GetCard(id);
        }
        public void UpdateCard(MeasureModel cardModel)
        {
            cardRepository.UpdateCard(cardModel);
        }
    }
}
