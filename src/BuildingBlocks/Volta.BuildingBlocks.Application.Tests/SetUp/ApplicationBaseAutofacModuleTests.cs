using System.Reflection;
using Autofac;
using FluentAssertions;
using MediatR.Extensions.Autofac.DependencyInjection;
using Volta.BuildingBlocks.Test.AutoFac;
using Xunit;

namespace Volta.BuildingBlocks.Application.Tests.SetUp
{
    public class ApplicationBaseAutofacModuleTests : IClassFixture<ApplicationBaseAutofacModuleFixture>
    {
        private readonly ApplicationBaseAutofacModuleFixture fixture;

        public ApplicationBaseAutofacModuleTests(ApplicationBaseAutofacModuleFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void When_LoadingApplicationBaseAutoFacModule_RequestExecutorShouldBeRegistered()
        {
            // Arrange
            // Act
            // The above is done in the fixture

            // Assert
            this.fixture.Container.IsRegistered<IRequestExecutor>().Should().BeTrue();

        }

        [Fact]
        public void When_LoadingApplicationBaseAutoFacModule_AllRegisteredTypesResolve()
        {

            // Arrange
            Assembly asm = typeof(IRequestExecutor).Assembly;

            // Act and Assert in the below method
            this.fixture.Container.CheckAssemblyRegistersModulesAndResolvesInstancePerDependency(
            typeof(IRequestExecutor),
               builder =>
              {
                  builder.RegisterMediatR(asm);
              });
        }

    }

}
