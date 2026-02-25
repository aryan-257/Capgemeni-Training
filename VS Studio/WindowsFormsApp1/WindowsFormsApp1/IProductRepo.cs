using System.Collections.Generic;

namespace WindowsFormsApp1
{
	public interface IProductRepo : IRepo<Product>
	{
		List<Product> ShowAllProductByCategory(int catID);
		List<Product> SortProductByPriceAsc();
		List<Product> SortProductByPriceDesc();
		List<Product> GetTop3BudgetProduct();
	}
}