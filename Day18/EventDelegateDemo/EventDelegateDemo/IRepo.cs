 using System;
using System.Collections.Generic;
using System.Text;

namespace EventDelegateDemo
{
    public interface IRepo<T>
    {
        T SearchByID(int id);
        bool Add(T obj);
    }
}