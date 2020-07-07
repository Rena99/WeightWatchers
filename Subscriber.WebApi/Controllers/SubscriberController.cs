using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;

namespace Subscriber.WebApi.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberServices SubscriberServices;
        private readonly IMapper _mapper;
        

        public SubscriberController(IMapper mapper,ISubscriberServices subscriberServices)
        {
            SubscriberServices = subscriberServices;
            _mapper = mapper;
        }

        [HttpPost]
        public bool Post([FromBody] DTOSubscriber subscriber)
        {
            return SubscriberServices.NewSubscriber(_mapper.Map<UserModel>(subscriber));
        }
        [HttpPost("login")]
        public IActionResult PostLogin([FromQuery] string email, [FromQuery] string password)
        {
            if(SubscriberServices.checkSubscriberEmail(email,password)==0)
                return Unauthorized();
            return Ok(SubscriberServices.checkSubscriberEmail(email, password));

        }

    }
}
