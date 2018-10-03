using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HodorVault.Data.Services
{
    public interface IConnectionFactory
    {
        /// <summary>
        /// Executes sql command in async
        /// </summary>
        Task ExecuteAsync(Action<DbConnection> action);

        /// <summary>
        /// Get open database connection
        /// </summary>
        /// <returns></returns>
        Task<DbConnection> GetOpenConnectionAsync();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteAsync(Action<DbConnection> action)
        {
            using (var c = new SqlConnection(_connectionString))
            {
                await c.OpenAsync();
                action(c);
                c.Close();
            }
        }

        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            SqlConnection c = new SqlConnection(_connectionString);
            await c.OpenAsync();
            return c;
        }
    }
}
