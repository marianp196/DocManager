using DocumentManagement.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persitency
{
	public interface IDocReferencesRepository: IRepository<string, DocReferenceDto>
	{
		Task<IEnumerable<DocReferenceDto>> GetByDocID(string docId);
		Task<IEnumerable<DocReferenceDto>> GetByKey(string name, string key);
		Task<bool> Exists(string name, string key, string documnetId);
	}

	public class DocReferenceDto
	{
		public DocReferenceDto(string documentId, DocumentReference documentRefernece)
		{
			RefId = documentRefernece.Id;
			Value = documentRefernece.Key;
			Name = documentRefernece.Type;
			DocumentID = documentId;
		}

		public DocReferenceDto()
		{}

		public int? Id { get; set; }// wegen simpleCrud hier drin
		public string RefId { get; set; }
		public string DocumentID { get; set;}
		public string Name { get; set; }
		public string Value { get; set; }

		public DocumentReference GetDomain()
		{
			var dref = new DocumentReference();
			dref.Id = RefId;
			dref.Key = Value;
			dref.Type = Name;

			return dref;
		}
	}
}
