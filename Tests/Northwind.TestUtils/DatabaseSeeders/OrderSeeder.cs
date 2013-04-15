using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Northwind.TestUtils.DatabaseSeeders
{
    public class OrderSeeder
    {
        public static void SeedOrderStatistics(IDbConnection connection)
        {
            const string sql = "INSERT INTO Orders (OrderDate) VALUES (@orderDate)";

            var now = DateTime.Now;

            var orderDates = new List<object>()
                {
                    new {orderDate = now},
                    new {orderDate = now.AddDays(-1)},
                    new {orderDate = now.AddDays(-2)}
                };


            connection.Execute(sql, orderDates);
        }
    }
}
