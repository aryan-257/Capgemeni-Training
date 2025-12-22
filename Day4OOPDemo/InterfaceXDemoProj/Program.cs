// See https://aka.ms/new-console-template for more information
using InterfaceXDemoProj;

Console.WriteLine("Demo on Interface");

/*

IAdd m1=new MathClass();//Alok -Add

IAddSub m2=new MathClass();//Riya -Add,Sub

IAll m3=new MathClass();//Rajat -Add,Prod,Div,Sub

*/

//Approach 1

Product pObj=new Product();
pObj.ProdID=101;
pObj.Name="Borosil Flask";
pObj.Price=250;

//Approach 2
//Object Initializer

Product pObj1=new Product()
{
    ProdID=102,
    Name="Cello Pen",
    Price=15
};

//Conection Initializer

List<Product> productList=new List<Product>()
{
    new Product(){ProdID=201,Name="Notebook",Price=40},
    new Product(){ProdID=202,Name="Marker",Price=20},
    new Product(){ProdID=203,Name="Eraser",Price=5}
};

foreach(var item in productList)
{
    System.Console.WriteLine($"Product ID: {item.ProdID} Name: {item.Name} Price: {item.Price}");
}