using MediatR;

namespace Volta.BuildingBlocks.Application
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}