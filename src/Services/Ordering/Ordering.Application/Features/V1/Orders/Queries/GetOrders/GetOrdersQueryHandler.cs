using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
        }

        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderEnitites = await _orderRepository.GetOrdersByUserName(request.UserName);
            var orderList = _mapper.Map<List<OrderDto>>(orderEnitites);

            return new ApiSuccessResult<List<OrderDto>>(orderList);
        }
    }
}
