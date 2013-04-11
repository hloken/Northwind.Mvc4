namespace Northwind.Data.Entities
{
    public class OrderStatistics
    {
        public OrderStatistics(int numberOfOrders)
        {
            NumberOfOrders = numberOfOrders;
        }

        public int NumberOfOrders { get; private set; }
    }
}
