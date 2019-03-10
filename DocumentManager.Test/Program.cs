using System;
using DocumentManagement;
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
		}
	}
}
