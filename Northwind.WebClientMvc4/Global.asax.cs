using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Northwind.WebClientMvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles();

            //NorthwindDataIocInstaller.
        }
    }

    //public class NorthwindDataIocInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(
    //            Component.For<SalesStatisticsDataAdapter>().LifestylePerWebRequest()
    //            );
    //    }
    //}
}