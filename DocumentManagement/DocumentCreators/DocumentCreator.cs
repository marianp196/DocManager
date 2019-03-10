using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentCreators;
using DocumentManagement.Abstractions.DocumentServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentCreators
{
	public class DocumentCreator : IDocumentCreator
	{
		public DocumentCreator(IDocumentService documentService, ISettings settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
		}

		public async Task<CreateResult> Create(DocMetaData metaData, 
			IEnumerable<DocumentReference> references, 
			FileCreate fileData)
		{
			if (metaData == null)
				throw new ArgumentNullException(nameof(metaData));
			if (fileData == null)
				throw new ArgumentNullException(nameof(fileData));
			if (fileData.Stream == null)
				throw new ArgumentNullException(nameof(fileData.Stream));

			var doc = createDoc(metaData, references);
			var managedFile = new MangedFile();
			managedFile.InRootPath = true;
			managedFile.FileName = Guid.NewGuid().ToString();
			managedFile.Extension = fileData.Extension;
			doc.MangedFile = managedFile;
			doc.MetaData = metaData;

			await createFileInDocMangerDirectory(fileData.Stream, managedFile);//ToDo Exception Handling verbessern

			try
			{
				var id = await _documentService.Create(doc);
				return new CreateResult(id);
			}
			catch(Exception e)
			{
				//ToDo Datei wieder entfernen
				throw e;
			}
					
		}

		private Document createDoc(DocMetaData metaData, IEnumerable<DocumentReference> references)
		{
			var document = new Document();
			document.Created = DateTime.Now;
			document.Updated = DateTime.Now;
			document.References = references;
			document.MetaData = metaData;
			return document;
		}

		private async Task<bool> createFileInDocMangerDirectory(Stream createFile, MangedFile mangedFile)
		{
			FileStream file = File.Create(getPath(mangedFile));
			await createFile.CopyToAsync(file);
			return true;
		}

		private string getPath(MangedFile mangedFile)
		{
			var path =  Path.Combine(_settings.BasePath, mangedFile.FileName);
			var ex = mangedFile.Extension;
			if(ex != null && ex != "")
				path += (ex.StartsWith(".") ? "" : ".") + ex;
			return path;
		}

		private IDocumentService _documentService;
		private ISettings _settings;
	}
}
