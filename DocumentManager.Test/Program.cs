using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using DocumentManagement;
using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentDataServices;
using DocumentManagement.Abstractions.DocumentFileServices;
using DocumentManagement.Database;
using DocumentManagement.Persitency;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManager.Test
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.TryAddDocumentManagement(new Settings("C:\\Users\\marian\\Desktop\\DocManager")).TryAddDocManagerDatabse<ConnectionFactory>();
			var provider = serviceCollection.BuildServiceProvider();
			IDocumentFileService creator = provider.GetService<IDocumentFileService>();

			for (int i = 0; i < 10; i++)
				await machMal(creator);

			var docs = await creator.GetAll();
			await docs.AsList()[10].CopyTo("C:\\Users\\marian\\Desktop", "Halleluja2.png");
			foreach (var doc in docs)
				Console.WriteLine(doc.MangedFile.GetFileName());
			Console.Read();

		}

		private static async Task machMal(IDocumentFileService creator)
		{
			FileStream fs = File.Open("C:\\Users\\marian\\Pictures\\Unbenannt.PNG", FileMode.Open);

			try
			{
				var refs = new List<DocumentReference> { new DocumentReference { Key = Guid.NewGuid().ToString(), Type = "Contact" } };
				using (fs)
					await creator.Create(new DocMetaData(), refs, new FileCreate { Extension = "png", Stream = fs });
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Read();
			}
		}
	}
}
