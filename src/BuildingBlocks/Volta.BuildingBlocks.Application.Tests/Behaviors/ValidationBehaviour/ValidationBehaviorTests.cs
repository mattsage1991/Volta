using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Volta.BuildingBlocks.Application.Tests.Behaviors.ValidationBehaviour
{
    /// <summary>
    /// Unit tests for <see cref="ValidationBehavior"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidationBehaviorTests : IClassFixture<ValidationBehaviorFixture>
    {
        public ValidationBehaviorFixture fixture;

        [Fact]
        public void When_ValidationBehaviorHasNoValidatorsSet_NoValidationExceptionIsThrownAndNextIsInvoked()
        {
            // Arrange
            this.fixture = new ValidationBehaviorFixture();
            this.fixture.NextHandler.Setup(n => n.Invoke())
                .Returns(Task.FromResult(Unit.Value));

            // Act
            Func<Task> func = () => this.fixture.Subject.Handle(this.fixture.Request, CancellationToken.None, this.fixture.NextHandler.Object);
            this.fixture.SetupWithNoValidator();

            // Assert
            func.Should().NotThrow<ValidationException>();
            this.fixture.NextHandler.Verify(u => u.Invoke(), Times.Once);
        }

        [Fact]
        public void When_ValidationBehaviorHasNoErrors_NoValidationExceptionIsThrownAndNextIsInvoked()
        {
            // Arrange
            this.fixture = new ValidationBehaviorFixture();
            this.fixture.NextHandler.Setup(n => n.Invoke())
                .Returns(Task.FromResult(Unit.Value));

            // Act
            Func<Task> func = () => this.fixture.Subject.Handle(this.fixture.Request, CancellationToken.None, this.fixture.NextHandler.Object);
            this.fixture.SetupValidationClean(this.fixture.Setup);

            // Assert
            func.Should().NotThrow<ValidationException>();
            this.fixture.NextHandler.Verify(u => u.Invoke(), Times.Once);
        }

        [Fact]
        public void When_ValidationBehaviorHasErrors_ValidationExceptionIsThrownAndNextIsNotInvoked()
        {
            // Arrange
            this.fixture = new ValidationBehaviorFixture();
            this.fixture.NextHandler.Setup(n => n.Invoke())
                .Returns(Task.FromResult(Unit.Value));

            // Act
            Func<Task> func = () => this.fixture.Subject.Handle(this.fixture.Request, CancellationToken.None, this.fixture.NextHandler.Object);
            this.fixture.SetupValidationWithErrors(this.fixture.Setup);

            // Assert
            func.Should().Throw<ValidationException>();
            this.fixture.NextHandler.Verify(u => u.Invoke(), Times.Never);
        }
    }
}
