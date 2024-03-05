using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCrm.Data.Entities.AppUser;

namespace CommonCrm.Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task CreateAsync(T entity);
		Task<T> GetAsync(int id);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
		Task<List<T>> ToListAsync();

	}

}
