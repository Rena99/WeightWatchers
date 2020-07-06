using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Data.Models;
using Subscriber.Services.Interfaces;
using Subscriber.WebApi.DTO;

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly IMapper mapper;
        public CardController(ICardService cardService, IMapper mapper)
        {
            this.cardService = cardService;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public DTOCard GetCard(int id)
        {
            if (cardService.GetCard(id) == null)
            {
                throw new Exception("Item Not Found");
            }
            else return mapper.Map<DTOCard>(cardService.GetCard(id));

        }
    }
}
