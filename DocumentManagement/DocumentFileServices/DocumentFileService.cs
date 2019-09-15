using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentFileServices;
using DocumentManagement.Abstractions.DocumentDataServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentFileServices
{
	public class DocumentFileService : IDocumentFileService
	{
		public DocumentFileService(IDocumentDataService documentService, ISettings settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));

			if (_settings.BasePath == null || _settings.BasePath == "" || !Directory.Exists(_settings.BasePath))
				throw new ArgumentException("BasePath doesn't exist");
		}

		public async Task<CreateResult> Create(DocMetaData metaData,
			IEnumerable<DocumentReference> references,
			FileCreate fileData)
		{
			DocumentFileCreator creator = new DocumentFileCreator(_settings.BasePath);
			var document = await creator.CreateDocument(metaData, references, fileData);//Das ist noch nicht wirklich optimal :)

			var newID = await _documentService.Create(document);
			
			return new CreateResult(newID);
		}

		public async Task<IEnumerable<DocumentFile>> GetAll()
		{
			var documents = await _documentService.GetAll();
			var resultList = new List<DocumentFile>();
			foreach (var document in documents)
				resultList.Add(createDocumentFile(document));
			return resultList;
		}

		public async Task<DocumentFile> Get(string id)
		{
			if (id == null || id == "")
				throw new ArgumentException(nameof(id));
			var document = await _documentService.GetByID(id);
			return createDocumentFile(document);
		}

		public Task<DocumentFile> Delete(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<DocumentFile>> GetWhere(string Key, string id)
		{
			//var documents = await _documentService.;
			var resultList = new List<DocumentFile>();
			/*foreach (var document in documents)
				resultList.Add(await createDocumentFile(document));*/
			return resultList;
		}

		private DocumentFile createDocumentFile(Document doc)
		{
			if(doc.MangedFile?.GetFileName() == null)
				return new DocumentFile(doc, null);

			var path = doc.MangedFile?.GetFileName();
			if (doc.MangedFile.InRootPath)
			{
				path = Path.Combine(_settings.BasePath, doc.MangedFile.GetFileName());
			}

			if (!File.Exists(path))
				path = null;

			return new DocumentFile(doc, path);
		}
	

		private IDocumentDataService _documentService;
		private ISettings _settings;
	}
}
