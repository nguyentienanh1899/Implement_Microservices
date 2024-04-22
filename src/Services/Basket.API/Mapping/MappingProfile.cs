using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.IntegrationEvents.Events;

namespace Basket.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}
