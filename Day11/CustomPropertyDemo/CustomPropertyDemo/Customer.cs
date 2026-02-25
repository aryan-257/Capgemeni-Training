using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPropertyDemo
{
    class Customer
    {
        List<Order> orderList;
        public Customer()
        {
            orderList = new List<Order>();
        }


        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Address MyProperty { get; set; }
        public Address ShippingAddr{ get; set; }
        public Address BillingAddr { get; set; }
        public List<Order> MyOrders
        {
            get
            {
                return orderList; 
            }
            set
            {
              orderList = value;
            }
        }
    }
}
