using System;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Volta.BuildingBlocks.Test.Mocks;
using Xunit;

namespace Volta.BuildingBlocks.Application.Tests.Behaviors.UnitOfWork
{
    /// <summary>
    /// UnitOfWorkTransactionBehaviorTests
    /// </summary>
    public partial class UnitOfWorkTransactionBehaviorTests : IClassFixture<UnitOfWorkFixture>
    {
        private UnitOfWorkFixture fixture;

        [Fact]
        public async Task When_NextHandlerCompletes_UnitOfWorkCompletes()
        {
            // Arrange
            this.fixture = new UnitOfWorkFixture();
            this.fixture.NextHandler.Setup(n => n.Invoke())
                .Returns(Task.FromResult(this.fixture.Response));

            // Act
            await this.fixture.Subject.Handle(this.fixture.Request, CancellationToken.None, this.fixture.NextHandler.Object);

            // Assert
            this.fixture.UnitOfWorkMock.Verify(u => u.Complete(CancellationToken.None));
        }

        [Fact]
        public void When_NextHandlerThrowsDbUpdateException_UnitOfWorkDoesNotComplete()
        {
            // Arrange
            this.fixture = new UnitOfWorkFixture();
            this.fixture.NextHandler.Setup(n => n.Invoke())
                .Throws<DbUpdateException>();

            // Act
            Func<Task<MockResponse>> func = async () => await this.fixture.Subject.Handle(this.fixture.Request, CancellationToken.None, this.fixture.NextHandler.Object);

            // Assert
            func.Should().Throw<DbUpdateException>();
            this.fixture.UnitOfWorkMock.Verify(u => u.Complete(CancellationToken.None), Times.Never);
        }
    }
}
