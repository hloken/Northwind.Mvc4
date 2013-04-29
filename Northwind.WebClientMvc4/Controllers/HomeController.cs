using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Northwind.Data.SalesStatistics;

namespace Northwind.WebClientMvc4.Controllers
{
    public class HomeController : Controller
    {
        private readonly SalesStatisticsDataAdapter _salesStatisticsDataAdapter;

        public HomeController(SalesStatisticsDataAdapter salesStatisticsDataAdapter)
        {
            _salesStatisticsDataAdapter = salesStatisticsDataAdapter;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            SalesStatistics salesStatistics = null;

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString))
            {
                sqlConnection.Open();

                salesStatistics = _salesStatisticsDataAdapter.GetStatistics(sqlConnection);
            }

            return View(salesStatistics);
        }



    }
}
