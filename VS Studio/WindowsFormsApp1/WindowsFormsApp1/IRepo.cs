using System.Collections.Generic;

namespace WindowsFormsApp1
{
	public interface IRepo<T>
	{
		bool AddData(T obj);
		bool UpdateData(int id, T obj);
		bool DeleteData(int id);
		List<T> ShowAll();
		T SearchByID(int id);
	}
}
