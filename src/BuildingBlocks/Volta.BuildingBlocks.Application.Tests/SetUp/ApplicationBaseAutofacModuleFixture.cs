using System;
using Autofac;
using Volta.BuildingBlocks.Application.Setup;

namespace Volta.BuildingBlocks.Application.Tests.SetUp
{
    public class ApplicationBaseAutofacModuleFixture : IDisposable
    {
        public readonly IContainer Container;
        public ApplicationBaseAutofacModuleFixture()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ApplicationBaseAutofacModule>();
            this.Container = containerBuilder.Build(Autofac.Builder.ContainerBuildOptions.IgnoreStartableComponents);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}
