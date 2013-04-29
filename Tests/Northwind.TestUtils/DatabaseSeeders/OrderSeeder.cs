using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Northwind.TestUtils.DatabaseSeeders
{
    public class OrderSeeder
    {
        public static void SeedOrdersForCustomers(IDbConnection connection)
        {
            const string sql = "INSERT INTO Orders (OrderDate, CustomerID) VALUES (@orderDate, @customerId)";

            var now = DateTime.Now;

            var orderDates = new List<object>()
                {
                    new {orderDate = now, customerId = CustomerSeeder.CustomerId1},
                    new {orderDate = now.AddDays(-1), customerId = CustomerSeeder.CustomerId1},
                    new {orderDate = now.AddDays(-1), customerId = CustomerSeeder.CustomerId2},
                    new {orderDate = now.AddDays(-2), customerId = CustomerSeeder.CustomerId2},
                    new {orderDate = now.AddDays(-2), customerId = CustomerSeeder.CustomerId3}
                };

            connection.Execute(sql, orderDates);
        }
    }
}
