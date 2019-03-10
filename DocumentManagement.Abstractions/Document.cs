using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentManagement.Abstractions
{
	public class Document
	{
		public string ID { get; set;}
		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public DocMetaData MetaData { get; set; }
		public MangedFile MangedFile { get; set; }
		public IEnumerable<DocumentReference> References { get; set; }
	}

	public class DocMetaData
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public DocumentType DocType { get; set; }
	}
}
