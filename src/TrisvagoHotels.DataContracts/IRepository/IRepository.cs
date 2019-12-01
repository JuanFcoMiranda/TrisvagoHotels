using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrisvagoHotels.DataContracts.IRepository {
	public interface IRepository<T> where T : class {
		ValueTask<T> GetById(int id);
		Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
		IAsyncEnumerable<T> GetAll();
		Task<int> CountAll();
		Task<int> CountWhere(Expression<Func<T, bool>> predicate);
		IAsyncEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> expression);
		void Add(T entity);
		Task Add(ICollection<T> entities);
		void Update(T entity);
		void Delete(T entity);
		Task Delete(int id);
		void Delete(ICollection<T> entities);
	}
}