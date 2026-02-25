using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{

    public class ProductUtility : IProductRepo
    {
        private SqlConnection con;
        private DataSet ds;
        private readonly string connectionString;

        public ProductUtility()
        {
            // Connection string - Update with your SQL Server details
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True";
            con = new SqlConnection(connectionString);
            ds = new DataSet();
        }

        public ProductUtility(string connString)
        {
            connectionString = connString;
            con = new SqlConnection(connectionString);
            ds = new DataSet();
        }

        #region IRepo Implementation

        bool IRepo<Product>.AddData(Product obj)
        {
            try
            {
                string query = "INSERT INTO Products (ProdID, ProdName, Price, Description) VALUES (@id, @name, @price, @desc)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", obj.ProdID);
                cmd.Parameters.AddWithValue("@name", obj.ProdName);
                cmd.Parameters.AddWithValue("@price", obj.Price);
                cmd.Parameters.AddWithValue("@desc", obj.Description ?? "");

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error adding product: {ex.Message}");
            }
        }

        bool IRepo<Product>.UpdateData(int id, Product obj)
        {
            try
            {
                string query = "UPDATE Products SET ProdName=@name, Price=@price, Description=@desc WHERE ProdID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", obj.ProdName);
                cmd.Parameters.AddWithValue("@price", obj.Price);
                cmd.Parameters.AddWithValue("@desc", obj.Description ?? "");

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error updating product: {ex.Message}");
            }
        }

        bool IRepo<Product>.DeleteData(int id)
        {
            try
            {
                string query = "DELETE FROM Products WHERE ProdID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error deleting product: {ex.Message}");
            }
        }

        Product IRepo<Product>.SearchByID(int id)
        {
            try
            {
                string query = "SELECT * FROM Products WHERE ProdID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Product product = null;
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                }

                reader.Close();
                con.Close();

                return product;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error searching product: {ex.Message}");
            }
        }

        List<int> IRepo<Product>.ShowAll()
        {
            try
            {
                List<int> productIds = new List<int>();
                string query = "SELECT ProdID FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productIds.Add(Convert.ToInt32(reader["ProdID"]));
                }

                reader.Close();
                con.Close();

                return productIds;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error retrieving products: {ex.Message}");
            }
        }

        #endregion

        #region IProductRepo Implementation

        public List<Product> GetAllProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
                con.Close();

                return products;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error retrieving all products: {ex.Message}");
            }
        }

        List<Product> IProductRepo.ShowAllProductByCategory(int catID)
        {
            try
            {
                List<Product> products = new List<Product>();
                string query = "SELECT * FROM Products WHERE CategoryID=@catID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@catID", catID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
                con.Close();

                return products;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error retrieving products by category: {ex.Message}");
            }
        }

        List<Product> IProductRepo.SortProductByPriceAsc()
        {
            try
            {
                List<Product> products = new List<Product>();
                string query = "SELECT * FROM Products ORDER BY Price ASC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
                con.Close();

                return products;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error sorting products: {ex.Message}");
            }
        }

        List<Product> IProductRepo.SortProductByPriceDesc()
        {
            try
            {
                List<Product> products = new List<Product>();
                string query = "SELECT * FROM Products ORDER BY Price DESC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
                con.Close();

                return products;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error sorting products: {ex.Message}");
            }
        }

        List<Product> IProductRepo.GetTop3BudgetProduct()
        {
            try
            {
                List<Product> products = new List<Product>();
                string query = "SELECT TOP 3 * FROM Products ORDER BY Price ASC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProdID = Convert.ToInt32(reader["ProdID"]),
                        ProdName = reader["ProdName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        Description = reader["Description"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
                con.Close();

                return products;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw new MyCustomException($"Error retrieving budget products: {ex.Message}");
            }
        }

        #endregion
    }
}
