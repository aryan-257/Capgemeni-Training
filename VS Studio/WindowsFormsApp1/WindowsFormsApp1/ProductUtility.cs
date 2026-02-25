using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
	public class ProductUtility : IProductRepo
	{
		SqlConnection con;

		public ProductUtility()
		{
            con = new SqlConnection(
    "Server=localhost\\SQLEXPRESS;Integrated Security=True;Database=ProductDB;TrustServerCertificate=True")

        }

		public bool AddData(Product obj)
		{
			throw new NotImplementedException();
		}

		public bool DeleteData(int id)
		{
			throw new NotImplementedException();
		}

		public Product SearchByID(int id)
		{
			throw new NotImplementedException();
		}

		public bool UpdateData(int id, Product obj)
		{
			throw new NotImplementedException();
		}

		public List<Product> ShowAll()
		{
			List<Product> list = new List<Product>();

			SqlCommand cmd = new SqlCommand("SELECT * FROM Products", con);

			try
			{
				con.Open();
				SqlDataReader dr = cmd.ExecuteReader();

				while (dr.Read())
				{
					list.Add(new Product
					{
						ProdID = (int)dr[0],
						Name = dr[1].ToString(),
						CatName = dr[2].ToString(),
						Price = Convert.ToSingle(dr[3]),
						CatDesc = dr[4].ToString()
					});
				}

				dr.Close();
			}
			finally
			{
				con.Close();
			}

			return list;
		}

		public List<Product> ShowAllProductByCategory(int catID)
		{
			throw new NotImplementedException();
		}

		public List<Product> SortProductByPriceAsc()
		{
			throw new NotImplementedException();
		}

		public List<Product> SortProductByPriceDesc()
		{
			throw new NotImplementedException();
		}

		public List<Product> GetTop3BudgetProduct()
		{
			throw new NotImplementedException();
		}
	}
}
