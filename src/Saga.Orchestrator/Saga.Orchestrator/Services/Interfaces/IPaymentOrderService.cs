using Shared.DTOs.Basket;
using Shared.DTOs.ScheduledJobs;

namespace Saga.Orchestrator.Services.Interfaces
{
    public interface IPaymentOrderService
    {
        Task<bool> PaymentOrderProcess(string username, PaymentOrderDto paymentOrderDto);
    }
}
