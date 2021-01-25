using System.Threading.Tasks;

namespace Volta.Stocks.Domain.Stocks
{
    public interface IStockExistsChecker
    {
        Task<bool> Exists(CompanyName companyName);
    }
}