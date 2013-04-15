using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Northwind.TestUtils.DatabaseSeeders
{
    public class CustomerSeeder
    {
        public static void SeedCustomerStatistics(IDbConnection connection)
        {
            const string sql = "INSERT INTO Customers (CustomerID, CompanyName) VALUES (@customerId, @companyName)";

            var companyNames = new List<object>
                {
                    new {customerId="ibm", companyName = "IBM"},
                    new {customerId="ms", companyName = "Microsoft"},
                    new {customerId="apple", companyName = "Apple"}
                };

            connection.Execute(sql, companyNames);
        }
    }
}