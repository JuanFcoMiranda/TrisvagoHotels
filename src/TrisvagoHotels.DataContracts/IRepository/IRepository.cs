using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrisvagoHotels.DataContracts.IRepository {
	public interface IRepository<T> where T : class {
		ValueTask<T> GetById(int id);
		Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate); 
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
		Task<int> CountAll();
		Task<int> CountWhere(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyCollection<T>> GetAllByCondition(Expression<Func<T, bool>> expression);
		Task Add(T entity);
		Task Add(ICollection<T> entities);
        Task Update(T entity);
		Task Delete(T entity);
		Task Delete(int id);
        Task Delete(ICollection<T> entities);
	}
}