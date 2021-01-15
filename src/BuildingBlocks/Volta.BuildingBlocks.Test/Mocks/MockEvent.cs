using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.BuildingBlocks.Test.Mocks
{
    public class MockEvent : DomainEvent
    {
        public MockEntity Entity { get; }

        public MockEvent(MockEntity mockEntity)
        {
            Entity = mockEntity ?? throw new ArgumentNullException(nameof(mockEntity));
        }
    }
}