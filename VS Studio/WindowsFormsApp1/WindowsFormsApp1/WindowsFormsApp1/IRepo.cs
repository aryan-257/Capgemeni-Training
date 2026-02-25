using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface IRepo<T>
    {
		bool AddData(T obj);
		bool UpdateData(int id , T obj);
		bool DeleteData(int id);
		List<int> ShowAll();
		T SearchByID(int id);
	
	}
}
