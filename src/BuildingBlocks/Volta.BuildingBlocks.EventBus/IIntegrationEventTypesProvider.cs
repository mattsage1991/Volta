using System;
using System.Collections.Generic;

namespace Volta.BuildingBlocks.EventBus
{
    public interface IIntegrationEventTypesProvider
    {
        List<Type> GetIntegrationEvents();
    }
}
