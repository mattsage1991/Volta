using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Volta.BuildingBlocks.Application.Behaviors;
using Volta.BuildingBlocks.Test.Mocks;

namespace Volta.BuildingBlocks.Application.Tests.Behaviors.ValidationBehaviour
{
    /// <summary>
    /// Fixture to support the unit tests for <see cref="ValidationBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidationBehaviorFixture : IDisposable
    {
        public MockCommandRequest Request;
        public MockResponse Response;
        public Mock<RequestHandlerDelegate<Unit>> NextHandler;

        public Mock<IValidator<MockCommandRequest>> ValidatorMock;
        public Mock<IEnumerable<IValidator<MockCommandRequest>>> ValidatorsMock;
        public ValidationBehavior<MockCommandRequest, Unit> Subject;

        public ValidationBehaviorFixture()
        {
            Request = new MockCommandRequest();
            NextHandler = new Mock<RequestHandlerDelegate<Unit>>();
            ValidatorsMock = new Mock<IEnumerable<IValidator<MockCommandRequest>>>();
        }

        public void SetupValidationClean(Action action)
        {
            ValidatorMock = new Mock<IValidator<MockCommandRequest>>();
            ValidatorMock.Setup(m => m.Validate(It.IsAny<MockCommandRequest>())).Returns(() => new ValidationResult());
            action();
        }

        public void SetupValidationWithErrors(Action action)
        {
            ValidatorMock = new Mock<IValidator<MockCommandRequest>>();
            ValidatorMock.Setup(m => m.Validate(It.IsAny<MockCommandRequest>())).Returns(() => new ValidationResult
            {
                Errors = { new ValidationFailure("Property", "Error") }
            });
            action();
        }

        public void Setup()
        {
            ValidatorsMock.Setup(m => m.GetEnumerator()).Returns(() => new List<IValidator<MockCommandRequest>> { ValidatorMock.Object }.GetEnumerator());

            Subject = new ValidationBehavior<MockCommandRequest, Unit>(ValidatorsMock.Object);
        }

        public void SetupWithNoValidator()
        {
            ValidatorsMock.Setup(m => m.GetEnumerator()).Returns(() => new List<IValidator<MockCommandRequest>>().GetEnumerator());

            Subject = new ValidationBehavior<MockCommandRequest, Unit>(ValidatorsMock.Object);
        }

        public void Dispose()
        {
        }
    }
}
