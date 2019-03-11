using DocumentManagement.Database.Repository;
using DocumentManagement.Database.Utilities;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Database.Documents
{
	public class DocumentRepository : Repository<string, DocumentDto>, IDocumentRepository
	{
		public DocumentRepository(IConnectionFactory connectionFactory) : 
			base(connectionFactory, "DocumentDto", "DocId")
		{
		}
	}
}
