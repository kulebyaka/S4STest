using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using S4C.DAL.Models;

namespace S4C.DAL.Repositories
{
	public interface IRepository<TItem> where TItem : class
	{
		Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<TItem> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default);
		Task AddAsync(TItem item, CancellationToken cancellationToken = default);
	}

	public class Repository<TDbContext, TItem> : IRepository<TItem> 
		where TItem : class
		where TDbContext : DbContext
	{
		protected TDbContext dbContext;
		
		public Repository(TDbContext context)
		{
			dbContext = context;
		}
		public async Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.Set<TItem>().ToListAsync();
		}

		public async Task<TItem> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
		{
			return await this.dbContext.Set<TItem>().FindAsync(id);
		}

		public async Task AddAsync(TItem item, CancellationToken cancellationToken = default)
		{
			this.dbContext.Set<TItem>().Add(item);
			_ = await this.dbContext.SaveChangesAsync();
		}
	}

	public class LicensesRepository : Repository<S4SContext, License>
	{
		public LicensesRepository(S4SContext context) : base(context)
		{
		}
	}


}