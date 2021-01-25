using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Models;
using Volta.Stocks.Application.Setup;

namespace Volta.Stocks.Application.Queries.GetStocksToBeUpdated
{
    public class GetStocksToBeUpdatedQueryHandler : IQueryHandler<GetStocksToBeUpdatedQuery, IEnumerable<StockModel>>
    {
        private readonly SqlConnectionString sqlConnectionString;

        public GetStocksToBeUpdatedQueryHandler(SqlConnectionString sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString ?? throw new ArgumentNullException(nameof(sqlConnectionString));
        }

        public async Task<IEnumerable<StockModel>> Handle(GetStocksToBeUpdatedQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                string sql = $"SELECT " +
                             $"[Id] AS [{nameof(StockModel.Id)}]" +
                             $",[CompanyName] AS [{nameof(StockModel.CompanyName)}]" +
                             $",[TickerSymbol] AS [{nameof(StockModel.TickerSymbol)}]" +
                             $"FROM [Stocks].[dbo].[Stocks] " +
                             $"WHERE DAY([LastUpdatedDate]) < DAY(GETDATE())";

                var result = await connection.QueryAsync<StockModel>(sql).ConfigureAwait(false);

                return result;
            }
        }
    }
}