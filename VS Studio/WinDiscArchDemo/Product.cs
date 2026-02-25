using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDiscArchDemo
{
    /// <summary>
    /// Entity Class Product
    /// </summary>
    public class Product
    {
        #region Fields

        int prodID;
        string prodName;
        float price;
        string desc;
        string category;
        
        #endregion

        #region Properties
        //CLR Properties
        public int ProdID
        {
            get { return prodID; }
            set
            {
                if (value <= 0 || value >= 999)
                {
                    throw new MyCustomException("Product ID is not valid");
                }
                else
                {
                    prodID = value;
                }
            }
        }

        public string ProdName
        {
            get { return prodName; }
            set
            {
                prodName = value;
            }
        }

        public float Price
        {
            get { return price; }
            set
            {
                price = value;
            }
        }
        public string Desc
        {
            get { return desc; }
            set
            {
                desc = value;
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
            }
        }
        #endregion
    }
}
