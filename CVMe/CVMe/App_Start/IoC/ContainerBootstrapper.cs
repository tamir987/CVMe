using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CVMe.App_Start.IoC.Plumbing;
using CVMe.Common.IoC;
using CVMe.Services.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CVMe.App_Start.IoC
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        ContainerBootstrapper(IWindsorContainer container)
        {
            Container = container;
        }

        public IWindsorContainer Container { get; }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));

            container
                .Install(FromAssembly.Containing<ServicesInstaller>())
                .Install(FromAssembly.Containing<CommonInstaller>())
                .Install(FromAssembly.This());

            //container.AddFacility<LoggingFacility>(f => f.LogUsing<Log4netFactory>().WithConfig("log4net.config"));

            var mvcControllerFactory = new WindsorControllerFactory(container);

            ControllerBuilder.Current.SetControllerFactory(mvcControllerFactory);

            var dependencyResolver = new WindsorDependencyResolver(container.Kernel);

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            DependencyResolver.SetResolver(dependencyResolver);

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}