using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Northwind.WebClientMvc4.Plumbing;

namespace Northwind.WebClientMvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles();

            BootstrapIocContainer();
            //NorthwindDataIocInstaller.
        }

        protected void Application_End()
        {
            _container.Dispose();    
        }

        private static void BootstrapIocContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
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