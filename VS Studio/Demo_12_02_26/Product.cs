using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_12_02_26
{
    /// <summary>
    /// Entity Class Product
    /// </summary>
    public class Product
    {
        #region Fields
        int prodID;
        string prodName;
        int price;
        string desc;
        #endregion

        #region Properties
        //CLR Properties
        public int ProdID
        {
            get { return prodID; }
            set 
            { 
                if(value<=0 || value>=999)
                {
                    throw new MyCustomException("Product ID is not Valid....");

                }
            }
        }
        #endregion
    }
}
