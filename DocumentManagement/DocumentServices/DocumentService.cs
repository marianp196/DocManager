using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentServices
{
	public class DocumentService : IDocumentService
	{
		public Task<string> Create(Document document)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Document document)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Document>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Document> GetByID(string id)
		{
			throw new NotImplementedException();
		}

		public Task Update(Document document)
		{
			throw new NotImplementedException();
		}
	}
}
