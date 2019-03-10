
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Persitency
{
	public interface IRepository<TKey, TObj>
	{
		Task Create(TObj obj);
		Task Update(TObj obj);
		Task Delete(TKey obj);

		Task<bool> Exists(TKey id);
		Task<TObj> Get(TKey id);
		Task<IEnumerable<TObj>> GetAll();
	}
}
