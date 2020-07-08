using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Trackingcontroller : ControllerBase
    {
        private readonly ITrackingService trackingService;
        private readonly IMapper mapper;
        public Trackingcontroller(ITrackingService trackingService, IMapper mapper)
        {
            this.trackingService = trackingService;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public List<DTOTracking> GetTrackings(int id, [FromQuery] int page, [FromQuery] int size)
        {
            List<TrackingModel> trackingModels = trackingService.GetTrackings(id, page, size);
            List<DTOTracking> trackings = new List<DTOTracking>();
            foreach (var item in trackingModels)
            {
                trackings.Add(mapper.Map<DTOTracking>(item));
            }
            return trackings;
        }
    }
}
