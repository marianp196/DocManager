using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DocumentManagement.Database.Utilities
{
	public interface IConnectionFactory
	{
		IDbConnection Get();
	}
}
