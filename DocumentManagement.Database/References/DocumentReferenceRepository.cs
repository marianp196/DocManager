using DocumentManagement.Database.Repository;
using DocumentManagement.Database.Utilities;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Database.References
{
	public class DocumentReferenceRepository : Repository<string, DocReferenceDto>, IDocReferencesRepository
	{
		public DocumentReferenceRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
		{
		}

		public Task<bool> Exists(string name, string key, string documnetId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<DocReferenceDto>> GetByKey(string name, string key)
		{
			throw new NotImplementedException();
		}
	}
}
