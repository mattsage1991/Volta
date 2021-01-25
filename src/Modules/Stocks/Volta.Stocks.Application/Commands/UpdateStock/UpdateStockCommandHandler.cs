using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.Application.Exceptions;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Application.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : ICommandHandler<UpdateStockCommand, Unit>
    {
        private readonly IStockRepository stockRepository;
        private readonly IStockLookup stockLookup;

        public UpdateStockCommandHandler(IStockRepository stockRepository, IStockLookup stockLookup)
        {
            this.stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            this.stockLookup = stockLookup ?? throw new ArgumentNullException(nameof(stockLookup));
        }
        
        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await this.stockRepository.GetById(new StockId(request.StockId), cancellationToken);

            if (stock is null)
                throw new NotFoundException();

            stock.Update(stockLookup);

            return Unit.Value;
        }
    }
}