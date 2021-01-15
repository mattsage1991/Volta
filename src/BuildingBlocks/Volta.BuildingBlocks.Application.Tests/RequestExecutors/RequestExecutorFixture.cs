using System;
using Autofac;
using MediatR;
using Moq;
using Volta.BuildingBlocks.Test.Mocks;

namespace Volta.BuildingBlocks.Application.Tests.RequestExecutors
{
    public class RequestExecutorFixture : IDisposable
    {
        public readonly Mock<IMediator> MediatorMock;
        public readonly Mock<ILifetimeScope> LifetimeScopeMock;
        public readonly RequestExecutor Subject;
        public readonly MockCommandRequest CommandRequest;
        public readonly MockQueryRequest QueryRequest;
        public readonly MockResponse Response;
        public RequestExecutorFixture()
        {
            this.MediatorMock = new Mock<IMediator>();
            this.LifetimeScopeMock = new Mock<ILifetimeScope>();
            this.Subject = new RequestExecutor(this.MediatorMock.Object, this.LifetimeScopeMock.Object);
        }

        public void Dispose()
        {
            this.LifetimeScopeMock.Object.Dispose();
        }
    }
}