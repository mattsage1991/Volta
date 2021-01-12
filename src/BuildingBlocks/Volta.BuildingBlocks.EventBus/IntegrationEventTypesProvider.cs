using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volta.BuildingBlocks.EventBus.Events;

namespace Volta.BuildingBlocks.EventBus
{
    public class IntegrationEventTypesProvider : IIntegrationEventTypesProvider
    {
        private readonly List<Type> types;

        public IntegrationEventTypesProvider(AssemblyName[] assemblyNames)
        {
            this.types = new List<Type>();
            foreach (var assemblyName in assemblyNames)
            {

                var types = Assembly.Load(assemblyName.FullName)
                    .GetTypes()
                    .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                    .ToList();
                this.types.AddRange(types);
            }
        }


        public List<Type> GetIntegrationEvents()
        {
            return types;
        }
    }
}
