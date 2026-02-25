using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Entity Class Product
    /// </summary>
    public class Product
    {
        #region Fields
        private int prodID;
        private string prodName;
        private int price;
        private string desc;
        #endregion

        #region Properties
        //CLR Properties
        public int ProdID
        {
            get
            {
                return prodID;
            }
            set
            {
                if (value < 0 || value > 999)
                {
                    throw new MyCustomException("Product ID must be between 0 and 999!");
                }
                prodID = value;
            }
        }

        public string ProdName
        {
            get { return prodName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new MyCustomException("Product Name cannot be empty!");
                }
                prodName = value;
            }
        }

        public int Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new MyCustomException("Price cannot be negative!");
                }
                price = value;
            }
        }

        public string Description
        {
            get { return desc; }
            set { desc = value; }
        }
        #endregion

        #region Constructors
        public Product()
        {
            prodName = "";
            desc = "";
        }

        public Product(int id, string name, int price, string description)
        {
            ProdID = id;
            ProdName = name;
            Price = price;
            Description = description;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"ID: {ProdID}, Name: {ProdName}, Price: {Price:C}, Desc: {Description}";
        }
        #endregion
    }
}
