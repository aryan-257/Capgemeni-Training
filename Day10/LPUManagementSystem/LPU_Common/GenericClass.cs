using System;
using System.Collections.Generic;
using System.Text;

namespace LPU_Common
{
    /// <summary>
    /// Custom Generic Class created for demo purpose
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClass<T>
    {
        /// <summary>
        /// Custom generic method for swapping
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public void SwapMe(ref T obj1, ref T obj2)
        {
            T temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }
    }
}
