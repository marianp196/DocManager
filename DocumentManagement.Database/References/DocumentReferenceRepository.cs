using DocumentManagement.Database.Repository;
using DocumentManagement.Database.Utilities;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace DocumentManagement.Database.References
{
	public class DocumentReferenceRepository : Repository<string, DocReferenceDto>, IDocReferencesRepository
	{
		public DocumentReferenceRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{
		}

		public async Task<bool> Exists(string name, string key, string documnetId)
		{
			var list = await _connection.QueryAsync<int>("select count(*) from DocReferenceDto where name = @name and key = @key");
			return list.First() > 0;
		}

		public async Task<IEnumerable<DocReferenceDto>> GetByKey(string name, string key)
		{
			return await _connection.QueryAsync<DocReferenceDto>("select * from DocReferenceDto where name = @name and key = @key");
		}
	}
}
