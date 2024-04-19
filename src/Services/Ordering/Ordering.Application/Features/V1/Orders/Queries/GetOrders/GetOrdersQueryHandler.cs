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
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        private const string MethodName = nameof(GetOrdersQueryHandler);
        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"Begin: {MethodName} - UserName: {request.UserName}");
            var orderEnitites = await _orderRepository.GetOrdersByUserName(request.UserName);
            var orderList = _mapper.Map<List<OrderDto>>(orderEnitites);
            _logger.Information($"Complete: {MethodName} - UserName: {request.UserName}");
            return new ApiSuccessResult<List<OrderDto>>(orderList);
        }
    }
}
