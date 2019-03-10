using System;
using System.Collections.Generic;
using System.IO;
using DocumentManagement;
using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentCreators;
using DocumentManagement.Database;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManager.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.TryAddDocumentManagement(new Settings(".\\DocManager"))
				.TryAddDocManagerDatabse<ConnectionFactory>();

			var provider = serviceCollection.BuildServiceProvider();

			IDocumentCreator creator = provider.GetService<IDocumentCreator>();


			FileStream fs = File.Open("C:\\Users\\marian\\Desktop\\test.txt", FileMode.Open);

			try
			{
				using (fs)
					creator.Create(new DocMetaData(), new List<DocumentReference>(),
						new FileCreate { Extension = "txt", Stream = fs });
			} catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Read();
			}
			
		}
	}
}
