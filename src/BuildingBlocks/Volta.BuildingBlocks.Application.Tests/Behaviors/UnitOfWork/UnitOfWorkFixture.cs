using System;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Volta.BuildingBlocks.Application.Behaviors;
using Volta.BuildingBlocks.Test.Mocks;

namespace Volta.BuildingBlocks.Application.Tests.Behaviors.UnitOfWork
{
    public class UnitOfWorkFixture : IDisposable
    {
        public ILogger<MockCommandRequest> LoggerMock;
        public Mock<IUnitOfWork> UnitOfWorkMock;
        public UnitOfWorkTransactionBehavior<MockCommandRequest, MockResponse> Subject;
        public MockCommandRequest Request;
        public MockResponse Response;
        public Mock<RequestHandlerDelegate<MockResponse>> NextHandler;

        public UnitOfWorkFixture()
        {
            LoggerMock = Mock.Of<ILogger<MockCommandRequest>>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            Subject = new UnitOfWorkTransactionBehavior<MockCommandRequest, MockResponse>(LoggerMock, UnitOfWorkMock.Object);
            Request = new MockCommandRequest();
            Response = new MockResponse();
            NextHandler = new Mock<RequestHandlerDelegate<MockResponse>>();
        }

        public void Dispose()
        {
        }
    }
}
