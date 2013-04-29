using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Northwind.Data.Customer;
using Northwind.Data.SalesStatistics;

namespace Northwind.Data
{
    public class IocInstaller : IWindsorInstaller 
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<CustomerDataAdapter>().LifestylePerWebRequest(),
                Component.For<SalesStatisticsDataAdapter>().LifestylePerWebRequest()
                );
        }
    }
}
