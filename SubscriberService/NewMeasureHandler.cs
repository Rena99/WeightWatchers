using AutoMapper;
using MessagesClasses;
using NServiceBus;
using NServiceBus.Logging;
using Subscriber.Data;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubscriberService
{
    class NewMeasureHandler : IHandleMessages<MeasureReceived>
    {
        static ILog log = LogManager.GetLogger<NewMeasureHandler>();
        //CardService cardService = new CardService(new CardRepository());
        ICardService cardService;
        IMapper mapper;
        public NewMeasureHandler(ICardService cardService, IMapper mapper)
        {
            this.cardService = cardService;
            this.mapper = mapper;
        }
        public Task Handle(MeasureReceived message, IMessageHandlerContext context)
        {
            log.Info($"Received New Measure, Id = {message.Id}");
            //cardService.UpdateCard(mapper.Map<MeasureModel>(message));
            var options = new SendOptions();
            options.RequiredImmediateDispatch();
            var updateMeasure = new UpdateMeasureStatus
            {
                Id = message.Id
            };
            return context.Send(updateMeasure, options);

        }

    }

}

