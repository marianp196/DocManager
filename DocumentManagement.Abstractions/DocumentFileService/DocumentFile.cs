using DocumentManagement.Abstractions.DocumentDataServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Abstractions.DocumentFileServices
{
	public class DocumentFile : Document
	{
		public DocumentFile(Document data, string filePath = null)
		{
			map(data, this);
			_filePath = filePath;
		}

		public bool FileExists { get => _filePath != null; }

		public Stream OpenReadStream()
		{
			if (_filePath == null || !FileExists)
				throw new FileNotFoundException(_filePath);
			return File.OpenRead(_filePath);
		}
			
		public async Task<string> CopyTo(string path, string fileName = null)
		{
			if (_filePath == null)
				throw new FileNotFoundException(_filePath);
			if (path == null || path == "")
				throw new ArgumentException(nameof(path));

			var goal = path;
			if (fileName == null)
				fileName = ID + (MangedFile.Extension.StartsWith(".") ? "" : ".") + MangedFile.Extension;
			goal = Path.Combine(goal, fileName);

			using(var read = OpenReadStream())
			{
				using (var write = File.OpenWrite(goal))
				{
					await read.CopyToAsync(write);
				}
			}

			return goal;
		}

		private void map<TFrom, TTo>(TFrom from, TTo to)
		{
			var typeFrom = typeof(TFrom);
			var typeTo = typeof(TTo);

			foreach(var prop in typeFrom.GetProperties())
			{
				var matching = typeTo.GetProperties().Where(p => p.Name == prop.Name).FirstOrDefault();
				if(matching != null &&  matching.GetType() == prop.GetType())
				{
					var value = prop.GetValue(from);
					matching.SetValue(to, value);
				}
			}
		}

		private string _filePath;
	}
}
