using Contracts.Services;
using Infrastructure.Services;
using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.OrderAggregate.Events;
using Serilog;
using Shared.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.EventHandlers
{
    public class OrdersDomainHandler : INotificationHandler<OrderCreatedEvent>, INotificationHandler<OrderDeletedEvent>
    {
        private readonly ILogger _logger;
        private readonly ISmtpEmailService _smtpEmailService;

        public OrdersDomainHandler(ILogger logger, ISmtpEmailService smtpEmailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));
        }
        public Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Ordering Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Ordering Domain Event: {DomainEvent}", notification.GetType().Name);
            var emailRequest = new MailRequest
            {
                ToAddress = notification.EmailAddress,
                Body = $"Your order detail:" + $"<p> OrderId: {notification.DocumentNo}</p>" + $"<p> Total: {notification.TotalPrice}</p>",
                Subject = $"Hi {notification.FullName}, your order was created"
            };
            try
            {
                await _smtpEmailService.SendEmailAsync(emailRequest, cancellationToken);
                _logger.Information($"Send created order to email {notification.EmailAddress}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Order {notification.Id} failed due to an error with the email serivce: {ex.Message}");
            }
        }


    }
}
