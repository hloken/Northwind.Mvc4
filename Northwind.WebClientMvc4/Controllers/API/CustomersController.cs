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

        // GET api/customer
        public IEnumerable<Customer> Get()
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                var customers = _customerDataAdapter.GetAll(sqlConnection);
                return customers;
            }
        }

        // GET api/customerdata/5

        public Customer Get(string customerId)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
               return  _customerDataAdapter.Get(customerId, sqlConnection);
            }
        }

        // POST api/customerdata

        public void Post([FromBody]Customer customer)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                _customerDataAdapter.Create(customer, sqlConnection);
            }
        }

        // PUT api/customerdata/5

        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customerdata/5

        public void Delete(string customerId)
        {
            using (var sqlConnection = CreateAndOpenSqlConnection())
            {
                _customerDataAdapter.Delete(customerId, sqlConnection);
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
