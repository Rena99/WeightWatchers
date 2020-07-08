using MessagesClasses;
using NServiceBus;
using NServiceBus.Logging;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using System;
using System.Threading.Tasks;

namespace Subscriber.WebApi
{
    class AddMeasureHandler : Saga<SagaData>,
                IAmStartedByMessages<MeasureReceived>,
                IAmStartedByMessages<UpdateMeasureStatus>
    {

        private readonly IMeasureService measureService;
        public AddMeasureHandler(IMeasureService measureService)
        {
            this.measureService = measureService;
        }
        public Task Handle(MeasureReceived message, IMessageHandlerContext context)
        {
            Data.isMeasureReceived = true;
            MeasureModel measureModel = new MeasureModel()
            {
                Weight = message.Weight,
                CardId = message.CardId,
                Status = "InProcess"
            };
            Data.MeasureId = measureService.PostMeasure(measureModel);
            return AddMeasure(context);

        }

        public Task Handle(UpdateMeasureStatus message, IMessageHandlerContext context)
        {
            Data.Succeeded = true;
            return AddMeasure(context);
        }
        private async Task AddMeasure(IMessageHandlerContext context)
        {
            if (Data.isMeasureReceived && Data.Succeeded)
            {
                measureService.UpdateStatus(Data.MeasureId);
                MarkAsComplete();
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<MeasureReceived>(message => message.Id.ToString())
                .ToSaga(sagaData => sagaData.SagaId);
            mapper.ConfigureMapping<UpdateMeasureStatus>(message => message.Id.ToString())
                .ToSaga(sagaData => sagaData.SagaId);
        }
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }

    public class SagaData: ContainSagaData
    {
        public string SagaId { get; set; }
        public bool isMeasureReceived { get; set; }
        public bool Succeeded { get; set; }
        public int MeasureId { get; set; }
    }

}
