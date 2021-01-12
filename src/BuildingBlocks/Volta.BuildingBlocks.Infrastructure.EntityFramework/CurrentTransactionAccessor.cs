using Volta.BuildingBlocks.Application;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework
{
    public class CurrentTransactionAccessor : ICurrentTransactionAccessor
    {
        private object currentTransaction;


        public object GetCurrentTransaction()
        {
            return currentTransaction;
        }

        public void SetCurrentTransaction(object transaction)
        {
            currentTransaction = transaction;
        }
    }
}
