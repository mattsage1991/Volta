using Volta.Stocks.Integration.Core.Common;

namespace Volta.Stocks.Integration.Core.Infrastructure.EFCore
{
    public class EFCoreTestSeedType : TestSeedType
    {
        /// <summary>
        /// An instance of a <see cref="EFCoreTestSeedType"/> class.
        /// </summary>
        public static EFCoreTestSeedType Instance = new EFCoreTestSeedType();

        private EFCoreTestSeedType()
            : base("EFCore")
        {
        }
    }
}