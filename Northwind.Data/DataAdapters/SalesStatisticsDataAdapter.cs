using System.Data;
using System.Linq;
using Dapper;
using Northwind.Data.Entities;

namespace Northwind.Data.DataAdapters
{
    public class SalesStatisticsDataAdapter
    {
        public SalesStatistics GetStatistics(IDbConnection connection)
        {
            const string sql = @"
            SELECT  (
                SELECT COUNT(*)
                FROM   Orders
            ) AS orderCount, (
                SELECT COUNT(*)
                FROM   Customers
            ) AS customerCount";

            var dbSalesStatistics = connection.Query<DbSalesStatistics>(sql).First();
            
            return new SalesStatistics(
                dbSalesStatistics.OrderCount, 
                dbSalesStatistics.CustomerCount);
        }

        public class DbSalesStatistics
        {
            public int OrderCount { get; set; }
            public int CustomerCount { get; set; }
        }
    }
}
