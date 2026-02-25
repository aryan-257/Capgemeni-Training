using System;
using System.Net.Sockets;

namespace DemoOnCollection;
public class Address
{
    public string FlatNo { get; set; }
    public string BuildingName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
}
public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }    
    public Address BillingAddress { get; set; }
    public Address ShippingAddress { get; set; }

}
