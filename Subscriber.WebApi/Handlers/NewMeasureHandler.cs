using AutoMapper;
using MessagesClasses;
using NServiceBus;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System.Threading.Tasks;

namespace Subscriber.WebApi
{
    class NewMeasureHandler : IHandleMessages<MeasureReceived>
    {
        ICardService cardService;
        IMapper mapper;
        public NewMeasureHandler(ICardService cardService, IMapper mapper)
        {
            this.cardService = cardService;
            this.mapper = mapper;
        }
        public Task Handle(MeasureReceived message, IMessageHandlerContext context)
        {
            cardService.UpdateCard(mapper.Map<MeasureModel>(message));
            var updateMeasure = new UpdateMeasureStatus
            {
                Id = message.Id
            };
            return context.SendLocal(updateMeasure);
        }
    }
}

