using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.BuildingBlocks.Domain.Events;
using Xunit;

namespace Volta.Stocks.Domain.UnitTests.SeedWork
{
    public abstract class TestBase
    {
        public static T AssertPublishedDomainEvent<T>(IEntity aggregate)
            where T : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static List<T> AssertPublishedDomainEvents<T>(IEntity aggregate)
            where T : IDomainEvent
        {
            var domainEvents = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().ToList();

            if (!domainEvents.Any())
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvents;
        }

        public static void AssertBrokenRule<TRule>(Action testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.Throws<BusinessRuleValidationException>(testDelegate);
            if (businessRuleValidationException != null)
            {
                businessRuleValidationException.BrokenRule.Should().BeOfType(typeof(TRule), message);
            }
        }
    }
}