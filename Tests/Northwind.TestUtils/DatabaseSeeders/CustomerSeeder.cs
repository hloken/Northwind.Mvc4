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

        public static string ContactName1 = "Rutger Hauer";
        public static string ContactName2 = "Steve Ballmer";
        public static string ContactName3 = "Steve Jobs";


        public static void SeedCustomers(IDbConnection connection)
        {
            const string sql = "INSERT INTO Customers (CustomerID, CompanyName, ContactName) VALUES (@customerId, @companyName, @contactName)";
            
            var companyNames = new List<object>
                {
                    new {customerId=CustomerId1, companyName = CustomerName1, contactName = ContactName1},
                    new {customerId=CustomerId2, companyName = CustomerName2, contactName = ContactName2},
                    new {customerId=CustomerId3, companyName = CustomerName3, contactName = ContactName3}
                };

            connection.Execute(sql, companyNames);
        }
    }
}