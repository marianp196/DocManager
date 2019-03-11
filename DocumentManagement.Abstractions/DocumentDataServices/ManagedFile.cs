using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Abstractions.DocumentDataServices
{
	public class MangedFile
	{
		public string Id { get; set; }
		public bool InRootPath { get; set; } = true;
		public string FileName { get; set;}
		public string Extension { get; set; }
	}
}
