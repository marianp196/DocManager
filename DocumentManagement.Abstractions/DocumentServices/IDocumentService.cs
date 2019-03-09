using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Abstractions.DocumentServices
{
	public interface IDocumentService
	{
		Task<string> Create(Document document);
		Task Update(Document document);
		Task Delete(Document document);
		Task<Document> GetByID(string id);
		Task<IEnumerable<Document>> GetAll();
	}
}
