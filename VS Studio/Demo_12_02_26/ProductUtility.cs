using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_12_02_26
{
    internal interface IRepo<T>
    {
        bool AddData(T obj);
        bool UpdateData(int id, T obj);
        bool DeleteData(int id);
        List<T> ShowAll(int id);
        T SearchByID(int id);

    }

    public interface IProductRepo : IRepo<Product>
    {

    }
    class ProductUtility
    {

    }
}
