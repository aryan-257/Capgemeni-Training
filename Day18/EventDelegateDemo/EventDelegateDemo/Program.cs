namespace EventDelegateDemo
{
    public delegate bool CreateRecord(Product p);

    public delegate void Caller(string str);
    internal class Program
    {
        public static void ShowMe(string str)
        {
            Console.WriteLine("ShowMe called!");
        }

        public void GenerateBill(string str)
        {
            Console.WriteLine("GenerateBill called!");
        }

        static void Main(string[] args)
        {
            //ProductRepo pRepo = new ProductRepo();

            //CreateRecord AddProduct = new CreateRecord(pRepo.Add);

            //AddProduct(new Product());

            Program pObj = new Program();

            Caller CallMe = new Caller(Program.ShowMe);
            CallMe += new Caller(pObj.GenerateBill);

            CallMe("LPU");

        }
    }
}
