using NUnit.Framework;
using Northwind.Data.DataAdapters;
using Northwind.TestUtils;
using Northwind.TestUtils.DatabaseSeeders;

namespace Northwind.Data.ComponentTests
{
    [TestFixture]
    public class SalesStatisticsDataAdapterTests : NorthwindDatabaseTestBase
    {
        [Test]
        public void GetStatistics_WithSomeOrders_ShouldReturnJustOrderStatistics()
        {
            // Arrange
            OrderSeeder.SeedOrderStatistics(SqlConnection);
            var dataAdapter = new SalesStatisticsDataAdapter();

            // Act
            var salesStatistics = dataAdapter.GetStatistics(SqlConnection);

            // Assert
            Assert.AreEqual(3, salesStatistics.NumberOfOrders);
            Assert.AreEqual(0, salesStatistics.NumberOfCustomers);
        }

        [Test]
        public void GetStatistics_WithSomeCustomers_ShouldReturnJustCustomerStatistics()
        {
            // Arrange
            CustomerSeeder.SeedCustomerStatistics(SqlConnection);
            var dataAdapter = new SalesStatisticsDataAdapter();

            // Act
            var salesStatistics = dataAdapter.GetStatistics(SqlConnection);

            // Assert
            Assert.AreEqual(3, salesStatistics.NumberOfCustomers);
            Assert.AreEqual(0, salesStatistics.NumberOfOrders);
        }
    }
}
