using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem7.LibraryFine
{
    public class FineManager
    {
        private SortedDictionary<decimal, LibraryUser> fines = new SortedDictionary<decimal, LibraryUser>();

        public void AddFine(LibraryUser member, int daysOverdue)
        {
            decimal fine = member.CalculateFine(daysOverdue);
            member.OutstandingFine += fine;

            if (fines.ContainsKey(member.OutstandingFine))
                fines.Remove(member.OutstandingFine - fine);

            fines[member.OutstandingFine] = member;
            Console.WriteLine($"Fine added: ₹{fine} for {member.Name} ({daysOverdue} days overdue). Total: ₹{member.OutstandingFine}");
        }

        public void PayFine(string memberId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidPaymentException("Payment amount must be positive");

            var member = fines.Values.FirstOrDefault(m => m.MemberId == memberId);
            if (member == null)
                throw new FineNotFoundException($"No outstanding fine found for member {memberId}");

            if (amount > member.OutstandingFine)
                throw new InvalidPaymentException($"Payment amount ₹{amount} exceeds outstanding fine ₹{member.OutstandingFine}");

            decimal oldFine = member.OutstandingFine;
            member.OutstandingFine -= amount;

            fines.Remove(oldFine);
            if (member.OutstandingFine > 0)
                fines[member.OutstandingFine] = member;

            Console.WriteLine($"Payment of ₹{amount} received from {member.Name}. Remaining: ₹{member.OutstandingFine}");
        }

        public void DisplayFines()
        {
            Console.WriteLine("\n=== Outstanding Fines (Sorted by Amount) ===");
            foreach (var kvp in fines)
            {
                Console.WriteLine($"₹{kvp.Key}: {kvp.Value.Name} ({kvp.Value.GetMemberType()})");
            }
        }
    }
}
