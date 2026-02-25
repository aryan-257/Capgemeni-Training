using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDiscArchDemo
{
    public class ProductUtility : IProductRepo
    {
        SqlConnection con;
        SqlDataAdapter adap1;
        DataSet ds;
        List<Product> prodList = null;

        public ProductUtility()
        {
            con = new SqlConnection();
            con.ConnectionString = "Server=.;Integrated Security=True;Database=LPU_Db;TrustServerCertificate=true";

        }

        public bool AddDatata(Product obj)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetTop3BudgetProduct()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetTop3CostlyProduct()
        {
            throw new NotImplementedException();
        }

        public Product SearchByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> ShowAll()
        {
            adap1 = new SqlDataAdapter("Select * From Product",con);
            adap1.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            adap1.Fill(ds, "Product");

            if (ds.Tables[0].Rows.Count > 0)
            {
                prodList = new List<Product>();
                foreach(DataRow drow in ds.Tables["Product"].Rows)
                {
                    Product p1 = new Product()
                    {
                        ProdID = Int32.Parse(drow[0].ToString()),
                        ProdName = drow[1].ToString(),
                        Category = drow[2].ToString(),
                        Price = Single.Parse(drow[3].ToString()),
                        Desc = drow[4].ToString(),

                    };
                    prodList.Add(p1);
                }

            }
            return prodList;
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

        public bool UpdateData(int id, Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
