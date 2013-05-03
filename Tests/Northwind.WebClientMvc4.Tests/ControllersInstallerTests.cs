using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;
using Northwind.WebClientMvc4.Controllers;
using Northwind.WebClientMvc4.Installers;

namespace Northwind.WebClientMvc4.Tests
{
    // Many of these tests are inspired if not directly copied from: http://docs.castleproject.org/Windsor.Windsor-tutorial-part-three-a-testing-your-first-installer.ashx

    [TestFixture]
    public class ControllersInstallerTests
    {
        private IWindsorContainer _containerWithControllers;

        [SetUp]
        public void SetUp()
        {
            _containerWithControllers = new WindsorContainer()
                        .Install(new ControllersInstaller());
        }

        [Test]
        public void All_controllers_implement_IController_Or_ApiController()
        {
            var allHandlers = GetAllHandlers(_containerWithControllers);
            var mvcControllerHandlers = GetHandlersFor(typeof(IController), _containerWithControllers);
            var apiControllerHandlers = GetHandlersFor(typeof (ApiController), _containerWithControllers);
            var allControllers = mvcControllerHandlers.Union(apiControllerHandlers);

            CollectionAssert.IsNotEmpty(allHandlers);
            CollectionAssert.AreEqual(allHandlers, allControllers);
        }
        
        [Test]
        public void All_mvc_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allMvcControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());

            var registeredMvcControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);

            Assert.AreEqual(allMvcControllers, registeredMvcControllers);
        }

        [Test]
        public void All_api_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allApiControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<ApiController>());

            var registeredApiControllers = GetImplementationTypesFor(typeof(ApiController), _containerWithControllers);

            Assert.AreEqual(allApiControllers, registeredApiControllers);
        }

        [Test]
        public void All_and_only_controllers_have_Controllers_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));

            var registeredMvcControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);
            var registeredApiControllers = GetImplementationTypesFor(typeof(IHttpController), _containerWithControllers);
            var allRegisteredControllers = registeredMvcControllers.Union(registeredApiControllers);

            CollectionAssert.AreEquivalent(allControllers, allRegisteredControllers);
        }

        [Test]
        public void All_and_only_controllers_live_in_Controllers_namespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));

            var registeredMvcControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);
            var registeredApiControllers = GetImplementationTypesFor(typeof(ApiController), _containerWithControllers);
            var allRegisteredControllers = registeredMvcControllers.Union(registeredApiControllers);

            CollectionAssert.AreEquivalent(allControllers, allRegisteredControllers);
        }

        [Test]
        public void All_mvc_controllers_are_transient()
        {
            var nonTransientMvcControllers = GetHandlersFor(typeof(IController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            CollectionAssert.IsEmpty(nonTransientMvcControllers);
        }

        [Test]
        public void All_api_controllers_are_transient()
        {
            var nonTransientApiControllers = GetHandlersFor(typeof(ApiController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            CollectionAssert.IsEmpty(nonTransientApiControllers);
        }

        [Test]
        public void All_mvc_controllers_expose_themselves_as_service()
        {
            var mvcControllersWithWrongName = GetHandlersFor(typeof(IController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            CollectionAssert.IsEmpty(mvcControllersWithWrongName);
        }

        [Test]
        public void All_Api_controllers_expose_themselves_as_service()
        {
            var apiControllersWithWrongName = GetHandlersFor(typeof(ApiController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            CollectionAssert.IsEmpty(apiControllersWithWrongName);
        }

        private IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
    }
}
