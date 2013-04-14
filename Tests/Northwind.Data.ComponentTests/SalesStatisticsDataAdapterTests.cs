using NUnit.Framework;
using Northwind.Data.DataAdapters;

namespace Northwind.Data.ComponentTests
{
    // TODO: should be changed to use local database created by/for tests
    [TestFixture]
    public class SalesStatisticsDataAdapterTests : DatabaseTestBase
    {
        [Test]
        public void GetStatistics_WithSomeOrdersAndCustomers_ShouldReturnValidStatistics()
        {
            // Arrange
            var dataAdapter = new SalesStatisticsDataAdapter();

            // Act
            var salesStatistics = dataAdapter.GetStatistics(SqlConnection);

            // Assert
            Assert.IsTrue(salesStatistics.NumberOfCustomers > 0);
            Assert.IsTrue(salesStatistics.NumberOfOrders > 0);
        }
    }
}
