namespace Saga.Orchestrator.OrderManager
{
    public enum OrderTransactionState
    {
        NotStarted,
        BasketGot,
        BasketGetFailed,
        BasketDeleted,
        OrderCreated,
        OrderCreatedFailed,
        OrderDeleted,
        OrderDeletedFailed,
        OrderGot,
        OrderGetFailed,
        InventoryUpdated,
        InventoryUpdateFailed,
        RollbackInventory,
        InventoryRollbackSuccess,
        InventoryRollbackFailed,
    }
}
