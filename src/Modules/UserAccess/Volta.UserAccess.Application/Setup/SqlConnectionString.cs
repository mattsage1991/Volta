using System;

namespace Volta.UserAccess.Application.Setup
{
    public class SqlConnectionString
    {

        public string ConnectionString { get; }

        public SqlConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace", nameof(connectionString));
            }

            ConnectionString = connectionString;
        }
    }
}