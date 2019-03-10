using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using DocumentManagement;
using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentCreators;
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
			serviceCollection.TryAddDocumentManagement(new Settings(".\\DocManager"))
				.TryAddDocManagerDatabse<ConnectionFactory>();

			var provider = serviceCollection.BuildServiceProvider();

			IDocumentCreator creator = provider.GetService<IDocumentCreator>();

			DocumentDto dto = new DocumentDto();
			dto.DocId = Guid.NewGuid().ToString();
			new ConnectionFactory().Get().Insert(dto);


			FileStream fs = File.Open("C:\\Users\\marian\\Desktop\\test.txt", FileMode.Open);

			try
			{
				using (fs)
					await creator.Create(new DocMetaData(), new List<DocumentReference>(),
						new FileCreate { Extension = "txt", Stream = fs });
			} catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Read();
			}
			
		}
	}
}
