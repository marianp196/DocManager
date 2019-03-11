using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagement.Abstractions;
using DocumentManagement.Abstractions.DocumentFileServices;
using DocumentManagement.Abstractions.DocumentDataServices;
using DocumentManagement.DocumentFileServices;
using DocumentManagement.DocumentFileServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocumentManagement
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection TryAddDocumentManagement(this IServiceCollection sc, Settings settings)
		{
			sc.TryAddTransient<IDocumentFileService, DocumentFileService>();
			sc.TryAddTransient<IDocumentDataService, DocumentDataService>();
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
