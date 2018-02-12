using System;
using ThreeDo.Models;
using System.Collections.Generic;

namespace ThreeDo.Repository
{
	public interface IRepository<T> where T : ThreeDo.Models.BaseEntity
	{
		T Add(T item);
		void Remove(Guid id);
		void Update(T item);
		T FindByID(Guid id);
		IEnumerable<T> FindAll();
	}
}
