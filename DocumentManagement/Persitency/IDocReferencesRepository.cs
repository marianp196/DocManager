using DocumentManagement.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persitency
{
	public interface IDocReferencesRepository: IRepository<string, DocReferenceDto>
	{
		Task<IEnumerable<DocReferenceDto>> GetByKey(string name, string key);
		Task<bool> Exists(string name, string key, string documnetId);
	}

	public class DocReferenceDto
	{
		public DocReferenceDto(string documentId, DocumentReference documentRefernece)
		{
			Id = documentRefernece.Id;
			Value = documentRefernece.Key;
			Name = documentRefernece.Type;
			DocumentID = documentId;
		}

		public DocReferenceDto()
		{}

		public string Id { get; set; }
		public string DocumentID { get; set;}
		public string Name { get; set; }
		public string Value { get; set; }

		public DocumentReference GetDomain()
		{
			var dref = new DocumentReference();
			dref.Id = Id;
			dref.Key = Value;
			dref.Type = Name;

			return dref;
		}
	}
}
