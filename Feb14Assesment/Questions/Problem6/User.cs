using System;

namespace Problem6.FlashSale
{
    public abstract class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        protected User(string id, string name, string email)
        {
            UserId = id;
            Name = name;
            Email = email;
        }

        public abstract void DisplayInfo();
    }

    public class Buyer : User
    {
        public decimal WalletBalance { get; set; }

        public Buyer(string id, string name, string email, decimal balance)
            : base(id, name, email)
        {
            WalletBalance = balance;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Buyer: {Name}, Balance: ₹{WalletBalance}");
        }

        public bool ValidateBid(decimal amount)
        {
            return amount <= WalletBalance;
        }
    }

    public class Seller : User
    {
        public string CompanyName { get; set; }

        public Seller(string id, string name, string email, string company)
            : base(id, name, email)
        {
            CompanyName = company;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Seller: {Name}, Company: {CompanyName}");
        }
    }
}
