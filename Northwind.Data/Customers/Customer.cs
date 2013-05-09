namespace Northwind.Data.Customers
{
    public class Customer
    {
        public Customer(string customerId, string companyName, string contactName)
        {
            CustomerId = customerId;
            CompanyName = companyName;
            ContactName = contactName;
        }

        public string CustomerId { get; private set; }
        public string CompanyName { get; private set; }
        public string ContactName { get; set; }
    }
}