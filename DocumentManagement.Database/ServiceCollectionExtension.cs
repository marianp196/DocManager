using DocumentManagement.Database.Documents;
using DocumentManagement.Database.References;
using DocumentManagement.Database.Utilities;
using DocumentManagement.Persitency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Database
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection TryAddDocManagerDatabse<TFactory>(this IServiceCollection sc)
			where TFactory : class, IConnectionFactory
		{
			sc.TryAddSingleton<IConnectionFactory, TFactory>();
			sc.TryAddTransient<IDocumentRepository, DocumentRepository>();
			sc.TryAddTransient<IDocReferencesRepository, DocumentReferenceRepository>();
			return sc;
		}
	}
}
