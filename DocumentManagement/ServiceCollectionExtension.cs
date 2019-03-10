using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentCreators;
using DocumentManagement.Abstractions.DocumentServices;
using DocumentManagement.DocumentCreators;
using DocumentManagement.DocumentServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocumentManagement
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection TryAddDocumentManagement(this IServiceCollection sc, Settings settings)
		{
			sc.TryAddTransient<IDocumentCreator, DocumentCreator>();
			sc.TryAddTransient<IDocumentService, DocumentService>();
			sc.TryAddTransient<ISettings>(c => settings);
			return sc;
		}
	}

	public class Settings : ISettings
	{
		public Settings(string basePath)
		{
			BasePath = basePath;
		}

		public string BasePath { get; }
	}
}
