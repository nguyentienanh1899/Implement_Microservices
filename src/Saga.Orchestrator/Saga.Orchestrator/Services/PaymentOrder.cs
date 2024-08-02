using AutoMapper;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Saga.Orchestrator.Services.Interfaces;
using ILogger = Serilog.ILogger;
using Shared.DTOs.Order;
using Shared.DTOs.Inventory;
using Shared.DTOs.Basket;

namespace Saga.Orchestrator.Services
{
    public class PaymentOrder : IPaymentOrderService
    {
        private readonly IOrderHttpRepository _orderHttpRepository;
        private readonly IBasketHttpRepository _basketHttpRepository;   
        private readonly IInventoryHttpRepository _inventoryHttpRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PaymentOrder(ILogger logger, IMapper mapper, IOrderHttpRepository orderHttpRepository, IBasketHttpRepository basketHttpRepository, IInventoryHttpRepository inventoryHttpRepository)
        {
            _orderHttpRepository = orderHttpRepository;
            _basketHttpRepository = basketHttpRepository;
            _inventoryHttpRepository = inventoryHttpRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> PaymentOrderProcess(string username, PaymentOrderDto paymentOrderDto)
        {
            // Get cart from BasketHttpRepositoty
            _logger.Information($"Start: Get Cart {username}");

            var cart = await _basketHttpRepository.GetBasket(username);
            if (cart == null) { return false; }

            _logger.Information($"End: Get Cart {username} success");

            // Create Order from OrderHttpRepository
            _logger.Information($"Start: Create Order");

            var order = _mapper.Map<CreateOrderDto>(paymentOrderDto);
            order.TotalPrice = cart.TotalPrice; //To avoid price errors, users can insert prices to send.
            // Get Order by orderId
            var orderId = await _orderHttpRepository.CreateOrder(order);
            if(orderId < 0) { return false; }

            var addedOrder = await _orderHttpRepository.GetOrder(orderId);

            _logger.Information($"Start: End Order Success - Order Id: {orderId} - Document No: {addedOrder.DocumentNo}");

            var inventoryDocumentNos = new List<string>();
            bool result;
            try
            {
                // Sales Item from InventoryHttpRepository
                foreach (var item in cart.Items)
                {
                    _logger.Information($"Start: Sale Item No: {item.ItemNo} - Quantity: {item.Quantity}");

                    var saleOrder = new SalesProductDto(addedOrder.DocumentNo, item.Quantity);
                    saleOrder.SetItemNo(item.ItemNo);

                    var documentNo = await _inventoryHttpRepository.CreateSalesOrder(saleOrder);
                    inventoryDocumentNos.Add(documentNo);

                    _logger.Information($"End: Sale Item No: {item.ItemNo} - Quantity: {item.Quantity} - Document No: {documentNo}");

                }

                //delete basket
                result = await _basketHttpRepository.DeleteBasket(username);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                // RollBack Payment Order
                RollbackPaymentOrder(username, addedOrder.Id, inventoryDocumentNos);
                result = false;
            }

            return result;
        }

        private async Task RollbackPaymentOrder(string username, long orderId, List<string> inventoryDocumentNos)
        {
            _logger.Information($"Start: RollbackCheckoutOrder for username: {username} - Order Id: {orderId} - inventory document nó:{String.Join(", ", inventoryDocumentNos)}");

            var deletedDocumentNos = new List<string>();
            // Delete order by order's id, order's document no
            _logger.Information($"Start: Delete Order Id: {orderId}");
            await _orderHttpRepository.DeleteOrder(orderId);
            _logger.Information($"End: Delete Order Id: {orderId} success");

            foreach (var documentNo in inventoryDocumentNos)
            {
                await _inventoryHttpRepository.DeleteOrderByDocumentNo(documentNo);
                deletedDocumentNos.Add(documentNo);
            }

            _logger.Information($"End: Deleted Inventory Document Nos: {String.Join(", ", inventoryDocumentNos)}");
        }
    }
}
