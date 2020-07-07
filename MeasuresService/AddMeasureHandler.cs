using AutoMapper;
using MessagesClasses;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeasuresService
{
    class AddMeasureHandler : Saga<SagaData>,
                IAmStartedByMessages<MeasureReceived>,
                IAmStartedByMessages<UpdateMeasureStatus>
    {
        static ILog log = LogManager.GetLogger<AddMeasureHandler>();


        //private readonly IMeasureService measureService;
        //public AddMeasureHandler(IMeasureService measureService)
        //{
        //    this.measureService = measureService;
        //}
        public Task Handle(MeasureReceived message, IMessageHandlerContext context)
        {
            log.Info($"Measure = {message.Id}");
            Data.isMeasureReceived = true;
            //MeasureModel measureModel = new MeasureModel()
            //{
            //    Weight = message.Weight,
            //    CardId = message.CardId,
            //    Status = "InProcess"

            //};
            //Data.MeasureId = measureService.PostMeasure(measureModel);
            return AddMeasure(context);

        }

        public Task Handle(UpdateMeasureStatus message, IMessageHandlerContext context)
        {
            log.Info($"Measure #{message.Id} was updated successfully.");
            Data.Succeeded = true;
            return AddMeasure(context);
        }
        private async Task AddMeasure(IMessageHandlerContext context)
        {
            if (Data.isMeasureReceived && Data.Succeeded)
            {
                //measureService.UpdateStatus(Data.MeasureId);
                log.Info($"Measure was updated successfully.");
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<MeasureReceived>(message => message.Id)
                .ToSaga(sagaData => sagaData.Id);
            mapper.ConfigureMapping<UpdateMeasureStatus>(message => message.Id)
                .ToSaga(sagaData => sagaData.Id);
        }

       
    }

    public class SagaData :
   ContainSagaData
    {
        public int Id { get; set; }
        public bool isMeasureReceived { get; set; }
        public bool Succeeded { get; set; }
        public int MeasureId { get; set; }
    }

}
