namespace Northwind.Data.Entities
{
    public class SalesStatistics
    {
        public SalesStatistics(int numberOfOrders, int numberOfCustomers)
        {
            NumberOfOrders = numberOfOrders;
            NumberOfCustomers = numberOfCustomers;
        }

        public int NumberOfOrders { get; private set; }
        public int NumberOfCustomers { get; set; }
    }
}
