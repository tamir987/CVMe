using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Services.IoC
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.
                    FromThisAssembly()
                    .Pick()
                    .WithServiceDefaultInterfaces()
                    .LifestyleSingleton());

            //container.Register(
            //    Classes.FromAssemblyContaining(typeof(ApiClient.ApiClient<>))
            //        .Pick()
            //        .WithServiceDefaultInterfaces()
            //        .LifestyleSingleton());

            //container
            //    .Install(FromAssembly.Containing<DataAccessInstaller>());
        }
    }
}
