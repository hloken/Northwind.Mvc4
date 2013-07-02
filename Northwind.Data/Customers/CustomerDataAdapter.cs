using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Northwind.Data.Customers
{
    public class CustomerDataAdapter
    {
        private const string _customerSelectStatement = @"
                SELECT  CustomerID, CompanyName, ContactName 
                FROM  Customers";

        public List<Customer> GetAll(IDbConnection connection)
        {
            const string sql = _customerSelectStatement;

            var dbCustomers = connection.Query<DbCustomer>(sql).ToList();
            var customers = dbCustomers.Select( MapToCustomer );

            return customers.ToList();
        }

        public Customer Get(string customerId, IDbConnection connection)
        {
            const string sql = _customerSelectStatement +
                               @" WHERE CustomerID = @customerId";

            return connection.Query<DbCustomer>(sql, new {customerId = customerId}).Select(MapToCustomer).FirstOrDefault();
        }

        public void Create(Customer newCustomer, IDbConnection connection)
        {
            const string sql = @"
                INSERT INTO Customers (CustomerID, CompanyName, ContactName)
                    VALUES (@customerID, @companyName, @contactName)";

            connection.Execute(sql, new
                {
                    customerId = newCustomer.CustomerId,
                    companyName = newCustomer.CompanyName,
                    contactName = newCustomer.ContactName
                });
        }

        public void Delete(string customerId, IDbConnection connection)
        {
            const string sql = @"
                DELETE FROM Customers
                WHERE CustomerId = @customerId
                ";

            connection.Execute(sql, new {customerId = customerId});
        }

        private Customer MapToCustomer(DbCustomer dbCustomer)
        {
            return new Customer(
                dbCustomer.CustomerID.Trim(), 
                dbCustomer.CompanyName, 
                dbCustomer.ContactName);
        }

        // ReSharper disable ClassNeverInstantiated.Local
        private class DbCustomer
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            // ReSharper restore UnusedAutoPropertyAccessor.Local

        }
        // ReSharper restore ClassNeverInstantiated.Local
    }
}