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
        private readonly IStockRepository stockRepository;

        public CreateStockCommandHandler(IStockLookup stockLookup, IStockRepository stockRepository)
        {
            this.stockLookup = stockLookup ?? throw new ArgumentNullException(nameof(stockLookup));
            this.stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
        }

        public async Task<Guid> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await Stock.Create(CompanyName.Of(request.CompanyName), TickerSymbol.Of(request.TickerSymbol), stockLookup);

            await stockRepository.Add(stock, cancellationToken);

            return stock.Id.Value;
        }
    }
}