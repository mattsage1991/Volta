using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using FluentAssertions;
using Volta.BuildingBlocks.Test.AutoFac.Enumerations;

namespace Volta.BuildingBlocks.Test.AutoFac
{
    public static class AutoFacExtentions
    {
        public static bool IsInstancePerDependency(this IComponentRegistration componentRegistration)
        {
            var registration = (ComponentRegistration)componentRegistration;
            return registration.Lifetime == CurrentScopeLifetime.Instance
                && registration.Sharing == InstanceSharing.None;
        }
        public static bool IsSingleInstance(this IComponentRegistration componentRegistration)
        {
            var registration = (ComponentRegistration)componentRegistration;
            return registration.Lifetime == RootScopeLifetime.Instance
                && registration.Sharing == InstanceSharing.Shared;
        }
        public static bool IsInstancePerLifetimeScope(this IComponentRegistration componentRegistration)
        {
            var registration = (ComponentRegistration)componentRegistration;
            return registration.Lifetime == CurrentScopeLifetime.Instance
                && registration.Sharing == InstanceSharing.Shared;
        }
        public static bool IsOwnedByLifetimeScope(this IComponentRegistration componentRegistration)
        {
            var registration = (ComponentRegistration)componentRegistration;
            return registration.Ownership == InstanceOwnership.OwnedByLifetimeScope;
        }
        public static bool IsExternallyOwned(this IComponentRegistration componentRegistration)
        {
            var registration = (ComponentRegistration)componentRegistration;
            return registration.Ownership == InstanceOwnership.ExternallyOwned;
        }
        public static void CheckAssemblyRegistersModulesAndResolvesInstancePerDependency(this IContainer container, Type assemblyTypeToResolve, Action<ContainerBuilder> configurationAction, LifeTypeScopeType lifeTimeScope = null)
        {
            // Arrange
            Assembly asm = assemblyTypeToResolve.Assembly;
            using (var scope = container.BeginLifetimeScope(
                configurationAction
            ))
            {
                foreach (var registration in container.ComponentRegistry.Registrations)
                {
                    foreach (var registrationService in registration.Services)
                    {
                        var registeredType = registrationService.Description;
                        // Only interested in the registered Assembly types
                        var types = asm.GetTypes();
                        if (types.Select(t => t.FullName).Contains(registeredType))
                        {
                            Type type = asm.GetType(registeredType);
                            // Act
                            var instance = scope.Resolve(type);
                            // Assert
                            instance.Should().NotBeNull();
                            LifeTypeScopeType.IsCorrectScopeSet(registration);
                        }
                    }
                }
            }
        }
    }
}
