using System;

namespace Problem9.ITSupport
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

        public abstract void ResolveTicket(SupportTicket ticket);
    }

    public class Employee : User
    {
        public string Department { get; set; }

        public Employee(string id, string name, string email, string dept)
            : base(id, name, email)
        {
            Department = dept;
        }

        public override void ResolveTicket(SupportTicket ticket)
        {
            Console.WriteLine($"Employee {Name} resolved ticket: {ticket.Title}");
        }
    }

    public class Admin : User
    {
        public string Role { get; set; }

        public Admin(string id, string name, string email, string role)
            : base(id, name, email)
        {
            Role = role;
        }

        public override void ResolveTicket(SupportTicket ticket)
        {
            Console.WriteLine($"Admin {Name} resolved critical ticket: {ticket.Title}");
        }
    }
}
