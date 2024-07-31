using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;
        public GetOrderByIdQueryHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        private const string MethodName = nameof(GetOrderByIdQueryHandler);
        public async Task<ApiResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"Begin: {MethodName} - Id: {request.Id}");

            var order = await _orderRepository.GetByIdAsync(request.Id);
            var orderDto = _mapper.Map<OrderDto>(order);

            _logger.Information($"Complete: {MethodName} - Id: {request.Id}");
            return new ApiSuccessResult<OrderDto>(orderDto);
        }
    }
}
