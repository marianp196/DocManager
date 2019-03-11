using DocumentManagement.Abstractions.DocumentDataServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Abstractions.DocumentFileServices
{
	public interface IDocumentFileService
	{
		Task<CreateResult> Create(DocMetaData metaData, 
			IEnumerable<DocumentReference> references, FileCreate stream);

		Task<IEnumerable<DocumentFile>> GetAll();

		Task<DocumentFile> Get(string id);

		Task<DocumentFile> Delete(string id);

		Task<IEnumerable<DocumentFile>> GetWhere(string Key, string id);
	}

	public class CreateResult
	{
		public CreateResult(string id)
		{
			CreatedId = id;
		}
		public string CreatedId { get; }
	}

	public class FileCreate
	{
		public string Extension { get; set; }
		public Stream Stream { get; set; }
	}

	
}
