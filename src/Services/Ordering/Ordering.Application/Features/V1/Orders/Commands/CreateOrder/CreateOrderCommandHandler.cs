using AutoMapper;
using Contracts.Services;
using MediatR;
using Serilog;
using Ordering.Application.Common.Interfaces;
using Shared.SeedWork;
using Ordering.Domain.Entities;
using Shared.Services.Email;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ISmtpEmailService _smtpEmailService;
        private ILogger _logger;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger, ISmtpEmailService smtpEmailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));   
        }
        private readonly string MethodName = "CreateOrderCommandHandler";
        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"Start: {MethodName} - UserName: {request.UserName}");
            var orderEntity = _mapper.Map<Order>(request);
            var addedOrder = await _orderRepository.CreateOrderAsync(orderEntity);
            await _orderRepository.SaveChangesAsync();
            _logger.Information($"Created order: Order {addedOrder.Id} is successfully created.");

            SendEmailAsync(addedOrder, cancellationToken);

            _logger.Information($"Complete: {MethodName} - UserName: {request.UserName}");
            return new ApiSuccessResult<long>(addedOrder.Id);
        }

        private async Task SendEmailAsync(Order order, CancellationToken cancellationToken)
        {
            var emailRequest = new MailRequest
            {
                ToAddress = order.EmailAddress,
                Body = "Order was created.",
                Subject = "Order was created"
            };
            try
            {
                await _smtpEmailService.SendEmailAsync(emailRequest, cancellationToken);
                _logger.Information($"Send created order to email {order.EmailAddress}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Order {order.Id} failed due to an error with the email serivce: {ex.Message}");
            }
        }
    }
}
