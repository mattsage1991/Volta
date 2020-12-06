using System;
using MediatR;

namespace Volta.BuildingBlocks.Application
{
    public interface ICommand
    {
    }

    public interface ICommand<T> : IRequest<T>, ICommand
    {
    }
}