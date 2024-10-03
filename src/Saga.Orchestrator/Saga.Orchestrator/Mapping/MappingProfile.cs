using AutoMapper;
using Shared.DTOs.Basket;
using Shared.DTOs.Inventory;
using Shared.DTOs.Order;

namespace Saga.Orchestrator.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<PaymentOrderDto, CreateOrderDto>();
            CreateMap<CartItemDto, SaleItemDto>();
        } 
    }
}
