using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Portfolios.Application.Commands.CreatePortfolios
{
    public class CreatePortfolioCommand : ICommand<Guid>
    {
        public CreatePortfolioCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}