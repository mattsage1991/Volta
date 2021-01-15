using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Application.Commands.UpdateStock
{
    //public class UpdateStockCommandHandler : ICommandHandler<UpdateStockCommand, Guid>
    //{
    //    private readonly IStockRepository stockRepository;
    //    private readonly IStockLookup stockLookup;

    //    public UpdateStockCommandHandler(IStockRepository stockRepository, IStockLookup stockLookup)
    //    {
    //        this.stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
    //        this.stockLookup = stockLookup ?? throw new ArgumentNullException(nameof(stockLookup));
    //    }


    //    public async Task<Guid> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    //    {
    //        var stock = await this.stockRepository.GetById(new StockId(request.StockId));

    //        stock.Update(stockLookup);

    //        return stock.Id.Value;
    //    }
    //}
}