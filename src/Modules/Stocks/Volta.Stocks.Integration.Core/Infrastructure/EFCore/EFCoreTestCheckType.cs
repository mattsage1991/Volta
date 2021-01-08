using Volta.Stocks.Integration.Core.Common;

namespace Volta.Stocks.Integration.Core.Infrastructure.EFCore
{
    public class EFCoreTestCheckType : TestCheckType
    {
        /// <summary>
        /// An instance of a <see cref="EFCoreTestCheckType"/> class.
        /// </summary>
        public static EFCoreTestCheckType Instance = new EFCoreTestCheckType();

        private EFCoreTestCheckType()
            : base("EFCore")
        {
        }
    }
}