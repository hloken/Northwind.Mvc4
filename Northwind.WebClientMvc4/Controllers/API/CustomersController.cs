using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json;
using Northwind.Data.Customer;

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
            using (var sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString))
            {
                sqlConnection.Open();

                var customers = _customerDataAdapter.GetAll(sqlConnection);
                return customers;
            }
        }

        // GET api/customerdata/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/customerdata
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customerdata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customerdata/5
        public void Delete(int id)
        {
        }
    }
}
