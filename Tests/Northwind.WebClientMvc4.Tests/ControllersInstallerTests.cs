using System;
using System.Linq;
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
        public void All_controllers_implement_IController()
        {
            var allHandlers = GetAllHandlers(_containerWithControllers);
            var controllerHandlers = GetHandlersFor(typeof(IController), _containerWithControllers);

            CollectionAssert.IsNotEmpty(allHandlers);
            CollectionAssert.AreEqual(allHandlers, controllerHandlers);
        }
        
        [Test]
        public void All_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());

            var registeredControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);

            Assert.AreEqual(allControllers, registeredControllers);
        }
        
        [Test]
        [Ignore] // Ignored, currently does not work with WebAPI controllers
        public void All_and_only_controllers_have_Controllers_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);
            
            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        [Ignore] // Ignored, currently does not work with WebAPI controllers
        public void All_and_only_controllers_live_in_Controllers_namespace()
        {
// ReSharper disable PossibleNullReferenceException
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
// ReSharper restore PossibleNullReferenceException
            var registeredControllers = GetImplementationTypesFor(typeof(IController), _containerWithControllers);

            Assert.AreEqual(allControllers, registeredControllers);
        }

        [Test]
        public void All_controllers_are_transient()
        {
            var nonTransientControllers = GetHandlersFor(typeof(IController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            CollectionAssert.IsEmpty(nonTransientControllers);
        }

        [Test]
        public void All_controllers_expose_themselves_as_service()
        {
            var controllersWithWrongName = GetHandlersFor(typeof(IController), _containerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            CollectionAssert.IsEmpty(controllersWithWrongName);
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
