using System.Data;
using System.Linq;
using Dapper;
using Northwind.Data.Entities;

namespace Northwind.Data.DataAdapters
{
    public class OrderStatisticsDataAdapter
    {
        public OrderStatistics GetStatistics(IDbConnection connection)
        {
            var orderCount = connection.Query<int>("select count(*) from Orders").First();

            return new OrderStatistics(orderCount);
        }
    }
}
