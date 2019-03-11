using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentDataServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Persitency
{
	public interface IDocumentRepository : IRepository<string, DocumentDto>
	{}

	public class DocumentDto
	{
		public DocumentDto(Document document)
		{
			assign(document);
		}

		public DocumentDto()
		{}

		public int? Id { get; set; } // ist hier drin wegen blödem simpleCrud framework... bis ich was besseres hab..scheißdreck
		public string DocId { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string FileName { get; set; }
		public string FileExtension { get; set; }


		private void assign(Document document)
		{
			DocId = document.ID;
			Created = document.Created;
			Updated = document.Updated;
			Title = document.MetaData?.Title;
			Text = document.MetaData?.Text;
			FileName = document.MangedFile?.FileName;
			FileExtension = document.MangedFile?.Extension;
		}

		public Document CreateDomain()
		{
			var document = new Document();
			
			document.ID = DocId;
			document.Created = Created;
			document.Updated = Updated;

			document.MetaData = new DocMetaData();
			document.MetaData.Title = Title;
			document.MetaData.Text = Text;

			document.MangedFile = new MangedFile();
			document.MangedFile.FileName = FileName;
			document.MangedFile.Extension = FileExtension;

			return document;
		}
	}
}
