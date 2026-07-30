using System.Collections.Generic;

class Transaction
{
    public string Id;
    public List<Transaction> LinkedTransactions;

    public Transaction(string id)
    {
        Id = id;
        LinkedTransactions = new List<Transaction>();
    }
}
