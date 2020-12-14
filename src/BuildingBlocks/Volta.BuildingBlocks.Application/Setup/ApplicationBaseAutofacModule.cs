using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Volta.BuildingBlocks.Application.Setup
{
    [ExcludeFromCodeCoverage]
    public class ApplicationBaseAutofacModule : Module
    {
        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestExecutor>().AsImplementedInterfaces().InstancePerDependency();

            base.Load(builder);
        }
    }
}
