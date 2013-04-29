using NUnit.Framework;
using Northwind.Data.Customer;
using Northwind.TestUtils;
using Northwind.TestUtils.DatabaseSeeders;

namespace Northwind.Data.ComponentTests
{
    [TestFixture]
    public class CustomerDataAdapterTests : NorthwindDatabaseTestBase
    {
        [Test]
        public void GetAll_ShouldReturnExpectedCustomersWithExpectedData()
        {
            var customerDataAdapter = new CustomerDataAdapter();

            var customers = customerDataAdapter.GetAll(SqlConnection);

            var customer1 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId1);
            Assert.AreEqual(CustomerSeeder.CustomerId1, customer1.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName1, customer1.CompanyName);

            var customer2 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId2);
            Assert.AreEqual(CustomerSeeder.CustomerId2, customer2.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName2, customer2.CompanyName);

            var customer3 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId3);
            Assert.AreEqual(CustomerSeeder.CustomerId3, customer3.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName3, customer3.CompanyName);
        }
    }
}