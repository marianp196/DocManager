using DocumentManagement.Database.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DocumentManager.Test
{
	public class ConnectionFactory : IConnectionFactory
	{
		public IDbConnection Get()
		{
			string conString = "Data Source = localhost; Initial Catalog = ThoughtRegistry; User=sa; Password=test";
			return new SqlConnection(conString);
		}
	}
}
