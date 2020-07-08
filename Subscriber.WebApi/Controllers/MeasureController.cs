using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MessagesClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMessageSession messageSession;
        public MeasureController(IMapper mapper, IMessageSession messageSession)
        {
            this.mapper = mapper;
            this.messageSession = messageSession;
        }
        [HttpPost]
        public void PostMeasure([FromBody] DTOMeasure measure)
        {
            messageSession.Publish(mapper.Map<MeasureReceived>(measure)).ConfigureAwait(false);
        }
    }
}
