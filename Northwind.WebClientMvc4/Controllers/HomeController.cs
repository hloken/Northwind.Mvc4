using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Northwind.Data.DataAdapters;
using Northwind.Data.Entities;

namespace Northwind.WebClientMvc4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            SalesStatistics salesStatistics = null;

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString))
            {
                sqlConnection.Open();

                salesStatistics = new SalesStatisticsDataAdapter().GetStatistics(sqlConnection);
            }

            return View(salesStatistics);
        }



    }
}
