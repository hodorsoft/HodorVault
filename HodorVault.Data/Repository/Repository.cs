using Dapper.Contrib.Extensions;
using HodorVault.Data.Models;
using HodorVault.Data.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace HodorVault.Data.Repository
{
    public interface IRepository<T> where T : class, IDbItem
    {
        /// <summary>
        /// Adds new item to database, modifies given items Id and returns it as int
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Id of the item/returns>
        Task<int> AddAsync(T item);

        /// <summary>
        /// Gets single item by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Get a single page of items
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageCount">Number of items in page</param>
        /// <returns></returns>
        Task<List<T>> GetPageAsync(int page, int pageCount);

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Updates item fields
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Deletes item from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }

    public class Repository<T> : IRepository<T> where T : class, IDbItem, new()
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDbConnection _connection;

        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(T item)
        {
            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                var id = await connection.InsertAsync(item);
                item.Id = id;
            }

            return item.Id;
        }

        public async Task DeleteAsync(int id)
        {
            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                await connection.DeleteAsync(new T { Id = id });
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                var items = await connection.GetAllAsync<T>();
                return items.ToList();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                return await connection.GetAsync<T>(id);
            }
        }

        public Task<List<T>> GetPageAsync(int page, int pageCount)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(T item)
        {
            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                return await connection.UpdateAsync(item);
            }
        }
    }
}
