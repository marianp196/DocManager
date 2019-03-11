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
		public Repository(IConnectionFactory connectionFactory, string tableName, string primKeyName)
		{
			_connection = connectionFactory.Get();
			TableName = tableName;
			PrimKeyName = primKeyName;
		}

		public async Task Create(TObj obj)
		{
			 await _connection.InsertAsync(obj);
		}

		public async Task Delete(TKey obj)
		{
			var sql = "delete from " + TableName + " where " + PrimKeyName + " = @ID ";
			await _connection.ExecuteAsync(sql,new { id = obj });
		}

		public async Task<bool> Exists(TKey id)
		{
			return await _connection.RecordCountAsync<TObj>("where id = @id", new { id }) > 0;
		}

		public async Task<TObj> Get(TKey id)
		{
			var sql = "select * from " + TableName + " where  " + PrimKeyName + " = @id";
			var result = await _connection.QueryAsync<TObj>(sql, new { id = id });
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<TObj>> GetAll()
		{
			return await _connection.GetListAsync<TObj>();
		}

		public async Task Update(TObj obj)
		{
			throw new NotImplementedException();
			//await _connection.UpdateAsync(); //UpdateAsync(obj);
		}

		protected IDbConnection _connection;
		protected string TableName { get; set; }
		protected string PrimKeyName { get; set; }
	}
}
