using System;

namespace WindowsFormsApp1
{
	public class Product
	{
		int prodID;

		public int ProdID
		{
			get { return prodID; }
			set
			{
				if (value < 0 || value > 999)
					throw new MyCustomException("Product ID is not valid!!");

				prodID = value;
			}
		}

		public string Name { get; set; }
		public string CatName { get; set; }
		public float Price { get; set; }
		public string CatDesc { get; set; }
	}
}
