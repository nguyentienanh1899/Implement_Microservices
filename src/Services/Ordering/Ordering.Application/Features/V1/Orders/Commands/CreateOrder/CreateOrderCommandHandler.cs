﻿using AutoMapper;
using Contracts.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private ILogger _logger;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger, ISmtpEmailService smtpEmailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        private readonly string MethodName = "CreateOrderCommandHandler";
        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"Start: {MethodName} - UserName: {request.UserName}");
            var orderEntity = _mapper.Map<Order>(request);
            _orderRepository.Create(orderEntity);
            orderEntity.AddedOrder();
            await _orderRepository.SaveChangesAsync();

            _logger.Information($"Created order: Order {orderEntity.Id} is successfully created.");


            _logger.Information($"Complete: {MethodName} - UserName: {request.UserName}");
            return new ApiSuccessResult<long>(orderEntity.Id);
        }


    }
}
