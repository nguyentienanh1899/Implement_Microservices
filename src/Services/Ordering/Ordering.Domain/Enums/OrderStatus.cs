namespace Ordering.Domain.Enums
{
    public enum OrderStatus
    {
        New = 1, // start with 1, 0 is used for filter all = 0
        Peding, //order peding, not activities for period time
        Paid,   //order is paid
        Shipping, //order is on the shipping
        Fulfilled, //order is fulfilled
    }
}
