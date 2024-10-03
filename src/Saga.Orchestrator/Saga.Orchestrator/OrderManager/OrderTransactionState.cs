namespace Saga.Orchestrator.OrderManager
{
    public enum OrderTransactionState
    {
        NotStarted,
        BasketGot,
        BasketGetFailed,
        OrderCreated,
        OrderCreatedFailed,
        OrderGot,
        OrderGetFailed,
        InventoryUpdated,
        InventoryUpdateFailed,
        InventoryRollback
    }
}
