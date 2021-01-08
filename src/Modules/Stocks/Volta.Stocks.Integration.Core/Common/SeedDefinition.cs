using System.Collections.Generic;
using System.Linq;

namespace Volta.Stocks.Integration.Core.Common
{
    public class SeedDefinition
    {
        /// <summary>
        /// Gets the associated <see cref="TestSeedType"/> seed type.
        /// </summary>
        public TestSeedType Type { get; }

        /// <summary>
        /// Gets a list of associated seed definition objects.
        /// </summary>
        public List<object> Objects { get; }

        /// <summary>
        /// Returns an empty list of <see cref="SeedDefinition"/> seed definitions.
        /// </summary>
        public static List<SeedDefinition> None
        {
            get { return new List<SeedDefinition>(); }
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SeedDefinition"/> class.
        /// </summary>
        /// <param name="type">A single <see cref="TestSeedType"/> seed type reference.</param>
        /// <param name="objects">A list of seed definition objects.</param>
        public SeedDefinition(TestSeedType type, List<object> objects)
        {
            Type = type;
            Objects = objects.ToList();
        }

        /// <summary>
        /// Returns a list of <see cref="SeedDefinition"/> records.
        /// </summary>
        /// <param name="type">A single <see cref="TestSeedType"/> seed type reference.</param>
        /// <param name="objects">A list of seed definition objects.</param>
        /// <returns></returns>
        public static List<SeedDefinition> For(TestSeedType type, List<object> objects)
        {
            return new List<SeedDefinition> {
                new SeedDefinition(type, objects)
            };
        }
    }
}