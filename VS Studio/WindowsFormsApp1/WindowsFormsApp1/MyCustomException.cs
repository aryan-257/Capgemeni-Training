using System;

namespace WindowsFormsApp1
{
	public class MyCustomException : Exception
	{
		public MyCustomException() : base() { }

		public MyCustomException(string errorMsg) : base(errorMsg) { }
	}
}
