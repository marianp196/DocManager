using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Persitency
{
	public interface IDocumentRepository : IRepository<string, DocumentDto>
	{}

	public class DocumentDto
	{
		public string ID { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string FileName { get; set; }
		public string FileExtension { get; set; }
	}
}
