using DocumentManagement.Database.Utilities;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
namespace DocumentManagement.Database.Repository
{
	public class Repository<TKey, TObj> : IRepository<TKey, TObj>
	{
		public Repository(IConnectionFactory connectionFactory)
		{
			_connection = connectionFactory.Get();
		}

		public async Task Create(TObj obj)
		{
			 await _connection.InsertAsync(obj);
		}

		public async Task Delete(TKey obj)
		{
			await _connection.DeleteAsync<TObj>(obj);
		}

		public async Task<bool> Exists(TKey id)
		{
			return await _connection.RecordCountAsync<TObj>("where id = @id", new { id }) > 0;
		}

		public async Task<TObj> Get(TKey id)
		{
			var result = await _connection.GetListAsync<TObj>(new { id });
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<TObj>> GetAll()
		{
			return await _connection.GetListAsync<TObj>();
		}

		public async Task Update(TObj obj)
		{
			await _connection.UpdateAsync(obj);
		}

		protected IDbConnection _connection;
	}
}
