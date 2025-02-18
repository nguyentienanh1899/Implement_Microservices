namespace Saga.Orchestrator.OrderManager
{
    public enum TriggerOrderAction
    {
        GetBasket,
        DeleteBasket,
        CreateOrder,
        GetOrder,
        DeleteOrder,
        UpdateInventory,
        DeleteInventory,
        RollbackInventory,
    }
}
