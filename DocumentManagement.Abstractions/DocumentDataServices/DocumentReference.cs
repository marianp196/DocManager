using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Abstractions.DocumentDataServices
{
	public class DocumentReference
	{
		public string Id { get; set;}
		public string Type { get; set; }
		public string Key { get; set; }
	}
}
