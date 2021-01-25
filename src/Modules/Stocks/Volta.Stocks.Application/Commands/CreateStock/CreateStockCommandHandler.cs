using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Application.Commands.CreateStock
{
    public class CreateStockCommandHandler : ICommandHandler<CreateStockCommand, Guid>
    {
        private readonly IStockLookup stockLookup;
        private readonly IStockExistsChecker stockExistsChecker;
        private readonly IStockRepository stockRepository;

        public CreateStockCommandHandler(IStockLookup stockLookup, IStockRepository stockRepository, IStockExistsChecker stockExistsChecker)
        {
            this.stockLookup = stockLookup ?? throw new ArgumentNullException(nameof(stockLookup));
            this.stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            this.stockExistsChecker = stockExistsChecker ?? throw new ArgumentNullException(nameof(stockExistsChecker));
        }

        public async Task<Guid> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = Stock.Create(CompanyName.Of(request.CompanyName), TickerSymbol.Of(request.TickerSymbol), stockExistsChecker, stockLookup);

            await stockRepository.Add(stock, cancellationToken);

            return stock.Id.Value;
        }
    }
}