using NUnit.Framework;
using Northwind.Data.SalesStatistics;
using Northwind.TestUtils;

namespace Northwind.Data.ComponentTests
{
    [TestFixture]
    public class SalesStatisticsDataAdapterTests : NorthwindDatabaseTestBase
    {
        [Test]
        public void GetStatistics_WithSomeOrders_ShouldReturnJustOrderStatistics()
        {
            // Arrange
            var dataAdapter = new SalesStatisticsDataAdapter();

            // Act
            var salesStatistics = dataAdapter.GetStatistics(SqlConnection);

            // Assert
            Assert.AreEqual(5, salesStatistics.NumberOfOrders);
        }

        [Test]
        public void GetStatistics_WithSomeCustomers_ShouldReturnJustCustomerStatistics()
        {
            // Arrange
            var dataAdapter = new SalesStatisticsDataAdapter();

            // Act
            var salesStatistics = dataAdapter.GetStatistics(SqlConnection);

            // Assert
            Assert.AreEqual(3, salesStatistics.NumberOfCustomers);
        }
    }
}
