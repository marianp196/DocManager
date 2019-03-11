using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentDataServices;
using DocumentManagement.Persitency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.DocumentFileServices
{
	public class DocumentDataService : IDocumentDataService
	{
		public DocumentDataService(IDocumentRepository documentRository, IDocReferencesRepository docReferenceRepository)
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

			//Check File exists

			await persist(document);

			return document.ID;
		}

		
		public async Task Delete(Document document)
		{
			await _documentRository.Delete(document.ID);

			var defs = await _docReferenceRepository.GetByDocID(document.ID);
			foreach (var def in defs)
				await _docReferenceRepository.Delete(def.RefId);
		}

		
		public async Task<IEnumerable<Document>> GetAll()
		{
			var dtos = await _documentRository.GetAll();
			var result = new List<Document>();
			foreach(var dto in dtos)
				result.Add(await createDocument(dto));
			return result;
		}

		public async Task<Document> GetByID(string id)
		{
			var docDto = await _documentRository.Get(id);
			return await createDocument(docDto);
		}

		public Task Update(Document document)
		{
			throw new NotImplementedException();
		}

		private async Task<Document> createDocument(DocumentDto dto)
		{
			var doc = dto.CreateDomain();

			var refListDtos = await _docReferenceRepository.GetByDocID(doc.ID);
			doc.References = refListDtos.Select(x => x.GetDomain());

			return doc;
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
