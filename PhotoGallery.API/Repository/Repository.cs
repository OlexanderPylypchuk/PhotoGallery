using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.API.Data;
using PhotoGallery.API.Repository.IRepository;

namespace PhotoGallery.API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		private DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
			_db = db;		
			_dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, string includeProperties = null, int pageSize = 5, int pageNumber = 1)
		{
			IQueryable<T> query = _dbSet;

			if (expression != null)
			{
				query = query.Where(expression);
			}

			if (pageSize > 0)
			{
				if (pageSize > 100)
				{
					pageSize = 100;
				}
				query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
			}

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (string include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}

			return query;
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (expression != null)
			{
				query = query.Where(expression);
			}

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (string include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}

			return await query.FirstOrDefaultAsync();
		}
	}
}
