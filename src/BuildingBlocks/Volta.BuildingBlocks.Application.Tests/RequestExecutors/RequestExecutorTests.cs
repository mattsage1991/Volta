using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Volta.BuildingBlocks.Application.Tests.RequestExecutors
{
    /// <summary>
    /// RequestExecutorTests
    /// </summary>
    public class RequestExecutorTests : IClassFixture<RequestExecutorFixture>
    {
        private RequestExecutorFixture fixture;

        [Fact]
        public async Task When_RequestExecutorExecuteCommandIsInvoked_BeginLifeTimeScopeIsCalledAsync()
        {
            // Arrange
            this.fixture = new RequestExecutorFixture();

            // Act
            await this.fixture.Subject.ExecuteCommand(this.fixture.CommandRequest, CancellationToken.None);

            // Assert
            this.fixture.LifetimeScopeMock.Verify(l => l.BeginLifetimeScope(), Times.Once);
        }

        [Fact]
        public async Task When_RequestExecutorExecuteCommandIsInvoked_MediatorSendIsCalledAsync()
        {
            // Arrange
            this.fixture = new RequestExecutorFixture();

            // Act
            await this.fixture.Subject.ExecuteCommand(this.fixture.CommandRequest, CancellationToken.None);

            // Assert
            this.fixture.MediatorMock.Verify(m => m.Send(this.fixture.CommandRequest, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task When_RequestExecutorExecuteQueryIsInvoked_MediatorSendIsCalledAsync()
        {
            // Arrange
            this.fixture = new RequestExecutorFixture();

            // Act
            await this.fixture.Subject.ExecuteQuery(this.fixture.QueryRequest, CancellationToken.None);

            // Assert
            this.fixture.MediatorMock.Verify(m => m.Send(this.fixture.QueryRequest, CancellationToken.None), Times.Once);
        }

    }
}
