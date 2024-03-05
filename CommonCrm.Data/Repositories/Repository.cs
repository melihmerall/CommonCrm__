using CommonCrm.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;

namespace CommonCrm.Data.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser?> _userManager;

		public Repository(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
		{
			_context = context;
			_userManager = _userManager;
		}

		public async Task CreateAsync(T entity)
		{
			_context.Set<T>().Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<T> GetAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Set<T>().FindAsync(id);
			if (entity != null)
			{
				_context.Set<T>().Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<List<T>> ToListAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}
		
	}
}
