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
        private readonly IStockLookup _stockLookup;
        private readonly IStockRepository _stockRepository;

        public CreateStockCommandHandler(IStockLookup stockLookup, IStockRepository stockRepository)
        {
            _stockLookup = stockLookup ?? throw new ArgumentNullException(nameof(stockLookup));
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
        }

        public async Task<Guid> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = Stock.Create(new StockId(Guid.NewGuid()), request.CompanyName, request.Symbol, _stockLookup);

            await _stockRepository.Add(stock, cancellationToken);

            return stock.Id;
        }
    }
}