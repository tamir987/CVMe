using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethod(typeof(WindsorActivator), "Shutdown")]
namespace CVMe.App_Start.IoC
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper _bootstrapper;

        public static void PreStart()
        {
            _bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        public static void Shutdown()
        {
            if (_bootstrapper != null)
                _bootstrapper.Dispose();
        }
    }
}