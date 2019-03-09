using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Abstractions
{
	public interface ISettings
	{
		string BasePath { get; }
	}
}
