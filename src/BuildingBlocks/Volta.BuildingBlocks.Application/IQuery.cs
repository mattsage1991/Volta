using MediatR;

namespace Volta.BuildingBlocks.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}