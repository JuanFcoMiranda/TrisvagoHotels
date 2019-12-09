using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Repositories {
	public sealed class EFRepository<T> : IRepository<T> where T : class {
		public EFRepository(MyDataContext context) {
			Context = context ?? throw new ArgumentNullException(nameof(context));
			DBSet = Context.Set<T>();
		}

		private DbSet<T> DBSet { get; set; }
		private DbContext Context { get; set; }
		public ValueTask<T> GetById(int id) => Context.Set<T>().FindAsync(id);
		
		public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => DBSet.FirstOrDefaultAsync(predicate);

		public Task<int> CountAll() => DBSet.CountAsync();
		
		public Task<int> CountWhere(Expression<Func<T, bool>> predicate) => DBSet.CountAsync(predicate);

		public async Task<IReadOnlyCollection<T>> GetAllByCondition(Expression<Func<T, bool>> expression) => await DBSet.Where(expression).AsNoTracking().ToListAsync();

		async Task<IReadOnlyList<T>> IRepository<T>.GetAll() => await DBSet.AsNoTracking().ToListAsync();

		public void Add(T entity) {
			EntityEntry dbEntity = Context.Entry(entity);
			if (dbEntity.State != EntityState.Detached)
				dbEntity.State = EntityState.Added;
			else {
				DBSet.AddAsync(entity);
			}
		}

		public Task Add(ICollection<T> entities) {
			foreach (var obj in entities) {
				EntityEntry dbEntity = Context.Entry(obj);
				if (dbEntity.State != EntityState.Detached)
					dbEntity.State = EntityState.Added;
			}
			return DBSet.AddRangeAsync(entities);
		}

		public void Update(T entity) {
			EntityEntry dbEntity = Context.Entry(entity);
			if (dbEntity.State == EntityState.Detached)
				DBSet.Attach(entity);
			dbEntity.State = EntityState.Modified;
			DBSet.Update(entity);
		}

		public void Delete(T entity) {
			EntityEntry dbEntity = Context.Entry(entity);
			if (dbEntity.State != EntityState.Deleted)
				dbEntity.State = EntityState.Deleted;
			else {
				DBSet.Attach(entity);
				DBSet.Remove(entity);
			}
		}

		public async Task Delete(int id) {
			T entity = await GetById(id);
			if (entity == null)
				return; 
			Delete(entity);
		}

		public void Delete(ICollection<T> entities) {
			foreach (var obj in entities) {
				EntityEntry dbEntity = Context.Entry(obj);
				if (dbEntity.State != EntityState.Deleted) {
					dbEntity.State = EntityState.Deleted;
				} else {
					DBSet.Attach(obj);
				}
			} 
			DBSet.RemoveRange(entities);
		}
	}
}