using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Northwind.Data.Customers;

namespace Northwind.WebClientMvc4.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly CustomerDataAdapter _customerDataAdapter;

        public CustomersController(CustomerDataAdapter customerDataAdapter)
        {
            _customerDataAdapter = customerDataAdapter;
        }

        // GET api/Customers
        public IEnumerable<Customer> Get()
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                var customers = _customerDataAdapter.GetAll(sqlConnection);
                return customers;
            }
        }

        // GET api/Customers/AFLK
        public Customer Get(string id)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                return _customerDataAdapter.Get(id, sqlConnection);
            }
        }

        // POST api/Customers
        public void Post([FromBody]Customer customer)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                _customerDataAdapter.Create(customer, sqlConnection);
            }
        }

        // PUT api/Customers/5
        public void Put(string id, [FromBody]Customer customer)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                _customerDataAdapter.Update(id, customer, sqlConnection);
            }
        }

        // DELETE api/Customers/5
        public void Delete(string id)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                _customerDataAdapter.Delete(id, sqlConnection);
            }
        }

        private static SqlConnection CreateAndOpenSqlConnection()
        {
            var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString);
            sqlConnection.Open();

            return sqlConnection;
        }
    }
}
