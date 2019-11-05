using System.Collections.Generic;

namespace TrisvagoHotels.DataContracts.IRepository {
	public interface IRepository<T> where T : class {
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Add(T entity);
		void Add(ICollection<T> entities);
		void Update(T entity);
		void Delete(T entity);
		void Delete(int id);
		void Delete(ICollection<T> entities);
	}
}