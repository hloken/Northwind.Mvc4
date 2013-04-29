namespace Northwind.Data.Customer
{
    public class Customer
    {
        public Customer(string customerId, string companyName)
        {
            CustomerId = customerId;
            CompanyName = companyName;
        }

        public string CustomerId { get; private set; }
        public string CompanyName { get; private set; } 
    }
}