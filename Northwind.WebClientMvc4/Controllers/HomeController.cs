using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Northwind.Data.DataAdapters;
using Northwind.Data.Entities;

namespace PlayingWithJquery.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            OrderStatistics statistics = null;

            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString))
            {
                sqlConnection.Open();

                statistics = new OrderStatisticsDataAdapter().GetStatistics(sqlConnection);
            }

            return View(statistics);
        }



    }
}
