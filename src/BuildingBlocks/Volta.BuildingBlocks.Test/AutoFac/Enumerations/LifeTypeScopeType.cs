using Autofac.Core;
using FluentAssertions;
using Volta.BuildingBlocks.Domain.Enumerations;

namespace Volta.BuildingBlocks.Test.AutoFac.Enumerations
{
    public class LifeTypeScopeType : Enumeration
    {
        public static readonly LifeTypeScopeType InstancePerDependency = new LifeTypeScopeType(1, "Instance Per Dependency");
        public static readonly LifeTypeScopeType SingleInstance = new LifeTypeScopeType(2, "Single Instance");
        public static readonly LifeTypeScopeType InstancePerLifetimeScope = new LifeTypeScopeType(3, "Instance Per LifetimeScope");
        public static readonly LifeTypeScopeType OwnedByLifetimeScope = new LifeTypeScopeType(4, "Owned By Lifetime Scope");
        public static readonly LifeTypeScopeType ExternallyOwned = new LifeTypeScopeType(5, "Externally Owned");

        private LifeTypeScopeType(int id, string name) : base(id, name)
        {
        }

        public static void IsCorrectScopeSet(IComponentRegistration registration, LifeTypeScopeType lifeTimeScope = null)
        {

            if (lifeTimeScope == null || lifeTimeScope == InstancePerDependency)
            {
                registration.IsInstancePerDependency().Should().BeTrue();
            }
            else if (lifeTimeScope == SingleInstance)
            {
                registration.IsSingleInstance().Should().BeTrue();
            }
            else if (lifeTimeScope == InstancePerLifetimeScope)
            {
                registration.IsInstancePerLifetimeScope().Should().BeTrue();
            }
            else if (lifeTimeScope == LifeTypeScopeType.OwnedByLifetimeScope)
            {
                registration.IsOwnedByLifetimeScope().Should().BeTrue();
            }
            else if (lifeTimeScope == LifeTypeScopeType.ExternallyOwned)
            {
                registration.IsExternallyOwned().Should().BeTrue();
            }
        }
    }
}
