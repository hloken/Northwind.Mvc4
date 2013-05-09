using NUnit.Framework;
using Northwind.Data.Customers;
using Northwind.TestUtils;
using Northwind.TestUtils.DatabaseSeeders;

namespace Northwind.Data.ComponentTests
{
    [TestFixture]
    public class CustomerDataAdapterTests : NorthwindDatabaseTestBase
    {
        private CustomerDataAdapter _customersDataAdapter;

        [SetUp]
        public void TestSetUp()
        {
            _customersDataAdapter = new CustomerDataAdapter();
        }

        [Test]
        public void GetAll_ShouldReturnExpectedCustomersWithExpectedData()
        {
            var customers = _customersDataAdapter.GetAll(SqlConnection);

            var customer1 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId1);
            CheckExpectedCustomer1(customer1);

            var customer2 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId2);
            CheckExpectedCustomer2(customer2);

            var customer3 = customers.Find(c => c.CustomerId == CustomerSeeder.CustomerId3);
            CheckExpectedCustomer3(customer3);
        }

        [Test]
        public void Get_WithValidCustomer_ShouldReturnCustomerWithExpectedData()
        {
            var actualCustomer1 = _customersDataAdapter.Get(CustomerSeeder.CustomerId1, SqlConnection);

            CheckExpectedCustomer1(actualCustomer1);
        }

        [Test]
        public void Create_ShouldUpdateTableWithCustomerData()
        {
            const string customerId = "SKN";
            var expectedCustomer = new Customer(
                customerId,
                "SkyNet",
                "T-800"
                );

            _customersDataAdapter.Create(expectedCustomer, SqlConnection);

            var actualCustomer = _customersDataAdapter.Get(customerId, SqlConnection);
            Assert.AreEqual(expectedCustomer.CustomerId, actualCustomer.CustomerId);
            Assert.AreEqual(expectedCustomer.CompanyName, actualCustomer.CompanyName);
            Assert.AreEqual(expectedCustomer.ContactName, actualCustomer.ContactName);
        }

        [Test]
        public void Delete_WithValidCustomerId_ShouldRemoveCustomerFromDatabase()
        {
            const string customerId = "SKN";
            var customer = new Customer(
                customerId,
                "SkyNet",
                "T-800"
                );

            _customersDataAdapter.Create(customer, SqlConnection);

            // Act
            _customersDataAdapter.Delete(customerId, SqlConnection);

            var actualCustomer = _customersDataAdapter.Get(customerId, SqlConnection);
            Assert.IsNull(actualCustomer);
        }

        private static void CheckExpectedCustomer1(Customer customer)
        {
            Assert.AreEqual(CustomerSeeder.CustomerId1, customer.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName1, customer.CompanyName);
            Assert.AreEqual(CustomerSeeder.ContactName1, customer.ContactName);
        }

        private static void CheckExpectedCustomer2(Customer customer)
        {
            Assert.AreEqual(CustomerSeeder.CustomerId2, customer.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName2, customer.CompanyName);
            Assert.AreEqual(CustomerSeeder.ContactName2, customer.ContactName);
        }

        private static void CheckExpectedCustomer3(Customer customer)
        {
            Assert.AreEqual(CustomerSeeder.CustomerId3, customer.CustomerId);
            Assert.AreEqual(CustomerSeeder.CustomerName3, customer.CompanyName);
            Assert.AreEqual(CustomerSeeder.ContactName3, customer.ContactName);
        }
    }
}