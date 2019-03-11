using DocumentManagement.Abstractions.DocumentDataServices;
using DocumentManagement.Abstractions.DocumentFileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentFileServices
{
	public class DocumentFileCreator
	{
		public DocumentFileCreator(string managerBasePath)
		{
			_basePath = managerBasePath ?? throw new ArgumentNullException(nameof(managerBasePath));

			if (_basePath == null || _basePath == "" || !Directory.Exists(_basePath))
				throw new ArgumentException("BasePath doesn't exist");
		}

		public async Task<Document> CreateDocument(DocMetaData metaData,
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

			return doc;
		}

		private async Task<bool> createFileInDocMangerDirectory(Stream createFile, MangedFile mangedFile)
		{
			FileStream file = File.Create(getPathFromManagedRoot(mangedFile));
			using (file)
			{
				await createFile.CopyToAsync(file);
			}
			return true;
		}

		private string getPathFromManagedRoot(MangedFile mangedFile)
		{
			if (!Directory.Exists(_basePath))
				throw new ArgumentException("BasePath doesn't exist");
			return Path.Combine(_basePath, mangedFile.GetFileName());
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

		private IDocumentDataService _documentService;
		private string _basePath;
	}
}
