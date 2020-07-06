using AutoMapper;
using Subscriber.Data.Models;
using Subscriber.Services.Models;
using Subscriber.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.WebApi.Mappings
{
    class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Data.Models.Subscriber, MSubscriber>();
            CreateMap<Card, MCard>();
            CreateMap<MUser, DTOCard>()
                .ForMember(d => d.FirstName, a => a.MapFrom(s => s.Subscriber.FirstName))
                .ForMember(d => d.LastName, a => a.MapFrom(s => s.Subscriber.LastName))
                .ForMember(d => d.Height, a => a.MapFrom(s => s.Card.Height))
                .ForMember(d => d.Weight, a => a.MapFrom(s => s.Card.Weight))
                .ForMember(d => d.BMI, a => a.MapFrom(s => s.Card.BMI));
        }

    }
}
