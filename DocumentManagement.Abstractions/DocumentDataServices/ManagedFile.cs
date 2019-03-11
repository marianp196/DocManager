using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Abstractions.DocumentDataServices
{
	public class MangedFile
	{
		public MangedFile()
		{}

		public string Id { get; set; }
		public bool InRootPath { get; set; } = true;
		public string FileName { get; set;}
		public string Extension { get; set; }

		public string GetFileName()
		{
			var name = FileName;
			if (Extension != null && Extension != "")
			{
				var extension = (Extension.StartsWith(".") ? "" : ".") + Extension;
				if (!name.EndsWith(extension))
					name += extension;
			}
			return name;
		}
	}
}
