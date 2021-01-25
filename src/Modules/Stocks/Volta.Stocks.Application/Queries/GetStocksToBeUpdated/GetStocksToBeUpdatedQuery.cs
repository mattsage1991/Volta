using System.Collections.Generic;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Models;

namespace Volta.Stocks.Application.Queries.GetStocksToBeUpdated
{
    public class GetStocksToBeUpdatedQuery : IQuery<IEnumerable<StockModel>>
    {
    }
}