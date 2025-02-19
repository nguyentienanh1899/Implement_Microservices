using AutoMapper;
using Contracts.Sagas.OrderManager;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Basket;
using Shared.DTOs.Inventory;
using Shared.DTOs.Order;
using ILogger = Serilog.ILogger;

namespace Saga.Orchestrator.OrderManager
{
    public class SagaOrderManager : ISagaOrderManager<PaymentOrderDto, OrderResponse>
    {
        private readonly IOrderHttpRepository _orderHttpRepository;
        private readonly IBasketHttpRepository _basketHttpRepository;
        private readonly IInventoryHttpRepository _inventoryHttpRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SagaOrderManager(ILogger logger, IMapper mapper, IOrderHttpRepository orderHttpRepository, IBasketHttpRepository basketHttpRepository, IInventoryHttpRepository inventoryHttpRepository)
        {
            _orderHttpRepository = orderHttpRepository;
            _basketHttpRepository = basketHttpRepository;
            _inventoryHttpRepository = inventoryHttpRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public OrderResponse CreateOrder(PaymentOrderDto input)
        {
            var orderStateMachine = new Stateless.StateMachine<OrderTransactionState, TriggerOrderAction>(OrderTransactionState.NotStarted);

            long orderId = -1;
            CartDto cart = null;
            OrderDto addedOrder = null;
            string? inventoryDocumentNo;

            orderStateMachine.Configure(OrderTransactionState.NotStarted)
                .PermitDynamic(TriggerOrderAction.GetBasket, () =>
                {
                    cart = _basketHttpRepository.GetBasket(input.UserName).Result;
                    return cart != null ? OrderTransactionState.BasketGot : OrderTransactionState.BasketGetFailed;
                });

            orderStateMachine.Configure(OrderTransactionState.BasketGot)
                .PermitDynamic(TriggerOrderAction.CreateOrder, () =>
                {
                    //input.TotalPrice = cart.TotalPrice;
                    var order = _mapper.Map<CreateOrderDto>(input);
                    order.TotalPrice = cart.TotalPrice;
                    orderId = _orderHttpRepository.CreateOrder(order).Result;
                    return orderId > 0 ? OrderTransactionState.OrderCreated : OrderTransactionState.OrderCreatedFailed;
                }).OnEntry(() => orderStateMachine.Fire(TriggerOrderAction.CreateOrder));

            orderStateMachine.Configure(OrderTransactionState.OrderCreated)
                .PermitDynamic(TriggerOrderAction.GetOrder, () =>
                {
                    addedOrder = _orderHttpRepository.GetOrder(orderId).Result;
                    return addedOrder != null ? OrderTransactionState.OrderGot : OrderTransactionState.OrderGetFailed;
                }).OnEntry(() => orderStateMachine.Fire(TriggerOrderAction.GetOrder));

            orderStateMachine.Configure(OrderTransactionState.OrderGot)
                .PermitDynamic(TriggerOrderAction.UpdateInventory, () =>
                {
                    var salesOrder = new SalesOrderDto()
                    {
                        OrderNo = addedOrder.DocumentNo,
                        SaleItems = _mapper.Map<List<SaleItemDto>>(cart.Items)
                    };

                    inventoryDocumentNo = _inventoryHttpRepository.CreateOrderSale(addedOrder.DocumentNo, salesOrder).Result;
                    return inventoryDocumentNo != null ? OrderTransactionState.InventoryUpdated : OrderTransactionState.InventoryUpdateFailed;
                }).OnEntry(() => orderStateMachine.Fire(TriggerOrderAction.UpdateInventory));

            orderStateMachine.Configure(OrderTransactionState.InventoryUpdated)
                .PermitDynamic(TriggerOrderAction.DeleteBasket, () =>
                {
                  var result = _basketHttpRepository.DeleteBasket(input.UserName).Result;
                    return result ? OrderTransactionState.BasketDeleted : OrderTransactionState.InventoryUpdateFailed;
                }).OnEntry(() => orderStateMachine.Fire(TriggerOrderAction.DeleteBasket));

            orderStateMachine.Fire(TriggerOrderAction.GetBasket);
            return new OrderResponse(orderStateMachine.State == OrderTransactionState.InventoryUpdated);
        }

        public OrderResponse RollbackOrder(PaymentOrderDto input)
        {
            return new OrderResponse(false);
        }
    }
}
