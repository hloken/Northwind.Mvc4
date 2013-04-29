using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Northwind.TestUtils.DatabaseSeeders
{
    public class CustomerSeeder
    {
        public static string CustomerId1 = "ibm";
        public static string CustomerId2 = "ms";
        public static string CustomerId3 = "apple";

        public static string CustomerName1 = "IBM";
        public static string CustomerName2 = "Microsoft";
        public static string CustomerName3 = "Apple";

        public static void SeedCustomers(IDbConnection connection)
        {
            const string sql = "INSERT INTO Customers (CustomerID, CompanyName) VALUES (@customerId, @companyName)";
            
            var companyNames = new List<object>
                {
                    new {customerId=CustomerId1, companyName = CustomerName1},
                    new {customerId=CustomerId2, companyName = CustomerName2},
                    new {customerId=CustomerId3, companyName = CustomerName3}
                };

            connection.Execute(sql, companyNames);
        }
    }
}