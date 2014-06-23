using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watchdog.data.context
{
	public interface IDbRepository<T>
	{
		T Get(int id);
		IEnumerable<T> Get();

		T Add(T @object);

		T Update(T @object);

		void Delete(int id);

		void Delete(T @object);
	}
}
