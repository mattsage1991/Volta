using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Commands.UpdateStock;
using Volta.Stocks.Application.Models;
using Volta.Stocks.Application.Setup;

namespace Volta.Stocks.Application.Commands.UpdateStocks
{
    public class UpdateStocksCommandHandler : ICommandHandler<UpdateStocksCommand, StocksUpdatedModel>
    {
        private readonly SqlConnectionString sqlConnectionString;
        private readonly ICommandsScheduler commandsScheduler;


        public UpdateStocksCommandHandler(SqlConnectionString sqlConnectionString, ICommandsScheduler commandsScheduler)
        {
            this.sqlConnectionString = sqlConnectionString ?? throw new ArgumentNullException(nameof(sqlConnectionString));
            this.commandsScheduler = commandsScheduler ?? throw new ArgumentNullException(nameof(commandsScheduler));
        }

        public async Task<StocksUpdatedModel> Handle(UpdateStocksCommand request, CancellationToken cancellationToken)
        {
            await using var connection = new SqlConnection(sqlConnectionString.ConnectionString);
            connection.Open();

            const string sql = "SELECT " +
                               "[Stocks].Id " +
                               "FROM [Stocks].Stocks";

            var stockIds = await connection.QueryAsync<Guid>(sql);

            var stockIdsList = stockIds.AsList();

            foreach (var stockId in stockIdsList)
            {
                await commandsScheduler.EnqueueAsync(
                    new UpdateStockCommand(
                        Guid.NewGuid(), stockId));
            }

            return new StocksUpdatedModel
            {
                NumberOfStocksUpdated = stockIdsList.Count
            };
        }
    }
}