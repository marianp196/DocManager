using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Abstractions.DocumentCreators
{
	public interface IDocumentCreator
	{
		Task<CreateResult> Create(DocMetaData metaData, 
			IEnumerable<DocumentReference> references, FileCreate stream);
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
