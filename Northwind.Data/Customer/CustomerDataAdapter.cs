using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Northwind.Data.Customer
{
    public class CustomerDataAdapter
    {
        public List<Customer> GetAll(IDbConnection connection)
        {
            const string sql = @"
                SELECT  CustomerID, CompanyName 
                FROM  Customers";

            var dbCustomers = connection.Query<DbCustomer>(sql).ToList();
            var customers = dbCustomers.Select( dbCustomer =>
                new Customer(dbCustomer.CustomerID.Trim(), dbCustomer.CompanyName));

            return customers.ToList();
        }

        // ReSharper disable ClassNeverInstantiated.Local
        private class DbCustomer
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local

        }
        // ReSharper restore ClassNeverInstantiated.Local

    }
}