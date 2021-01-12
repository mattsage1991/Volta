namespace Volta.BuildingBlocks.Application
{
    public interface ICurrentTransactionAccessor
    {
        void SetCurrentTransaction(object transaction);
        object GetCurrentTransaction();
    }
}
