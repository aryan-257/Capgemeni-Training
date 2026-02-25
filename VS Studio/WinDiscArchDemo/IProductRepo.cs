using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDiscArchDemo
{
    public interface IProductRepo : IRepo<Product>
    {
        List<Product> ShowAllProductByCategory(int catID);
        List<Product> SortProductByPriceAsc();
        List<Product> SortProductByPriceDesc();
        List<Product> GetTop3CostlyProduct();
        List<Product> GetTop3BudgetProduct();
    }
}
