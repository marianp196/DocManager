using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentServices;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentServices
{
	public class DocumentService : IDocumentService
	{
		public DocumentService(IDocumentRepository documentRository, IDocReferencesRepository docReferenceRepository)
		{
			_documentRository = documentRository;
			_docReferenceRepository = docReferenceRepository;
		}

		public async Task<string> Create(Document document)
		{

			if (document.ID != null && document.ID != "" && await _documentRository.Exists(document.ID))
				throw new Exception("id exists allready");
			else
				document.ID = Guid.NewGuid().ToString();

			//ToDo checkFileExists

			await persist(document);

			return document.ID;
		}

		
		public Task Delete(Document document)
		{
			throw new NotImplementedException();
		}

		public Task Update(Document document)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Document>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Document> GetByID(string id)
		{
			throw new NotImplementedException();
		}

		private async Task persist(Document document)
		{
			await checkReferences(document.References);

			var docDto = new DocumentDto(document);
			await _documentRository.Create(docDto);

			await createRefrences(document.ID, document.References);
		}

		private async Task checkReferences(IEnumerable<DocumentReference> references)
		{
			foreach (var docRef in references)
			{
				if (docRef.Type == null || docRef.Type == "")
					throw new ArgumentException(nameof(docRef.Key));

				if (docRef.Id == null || docRef.Id == "")
					docRef.Id = Guid.NewGuid().ToString();
				else if (await _docReferenceRepository.Exists(docRef.Id))
					throw new Exception("ref Id exists allready: " + docRef.Id);
			}
		}

		private async Task createRefrences(string docId ,IEnumerable<DocumentReference> references)
		{
			foreach(var docRef in references)	
				await _docReferenceRepository.Create(new DocReferenceDto(docId, docRef));
		}

		private IDocumentRepository _documentRository;
		private IDocReferencesRepository _docReferenceRepository;
	}
}
