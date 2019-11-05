using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrisvagoHotels.DataContext;
using TrisvagoHotels.DataContracts.IRepository;

namespace TrisvagoHotels.Repositories {
	public class EFRepository<T> : IRepository<T> where T : class {
		public EFRepository(MyDataContext context) {
			Context = context ?? throw new ArgumentNullException(nameof(context));
			DBSet = Context.Set<T>();
		}

		protected DbSet<T> DBSet { get; set; }
		protected DbContext Context { get; set; }

		public IEnumerable<T> GetAll() {
			return DBSet.AsEnumerable();
		}

		public virtual T GetById(int id) {
			return DBSet.Find(id);
		}

		public virtual T GetById(string id) {
			return DBSet.Find(id);
		}

		public void Add(T entity) {
			EntityEntry dbEntity = Context.Entry(entity);
			if (dbEntity.State != EntityState.Detached)
				dbEntity.State = EntityState.Added;
			else {
				DBSet.Add(entity);
			}
		}

		public void Add(ICollection<T> entities) {
			foreach (var obj in entities) {
				EntityEntry dbEntity = Context.Entry(obj);
				if (dbEntity.State != EntityState.Detached)
					dbEntity.State = EntityState.Added;
			}
			DBSet.AddRangeAsync(entities);
		}

		public void Update(T entity) {
			EntityEntry dbEntity = Context.Entry(entity);
			if (dbEntity.State == EntityState.Detached)
				DBSet.Attach(entity);
			dbEntity.State = EntityState.Modified;
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

		public virtual void Delete(int id) {
			T entity = GetById(id);
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