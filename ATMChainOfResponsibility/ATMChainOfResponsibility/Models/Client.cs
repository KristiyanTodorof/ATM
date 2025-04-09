using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Models
{
    public class Client
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public ClientType Type { get; set; }
        public List<Transaction> TransactionHistory { get; set; }

        public Client(string name, string accountNumber, ClientType type)
        {
            this.Name = name;
            this.AccountNumber = accountNumber;
            this.Type = type;
            this.TransactionHistory = new List<Transaction>();
        }
        public void AddTransaction(decimal amount, string transactionType, decimal newBalance)
        {
            TransactionHistory.Add(new Transaction 
            {
                Date = DateTime.Now,
                Amount = amount,
                Type = transactionType,
                NewBalance = newBalance

            });

        }
    }
}
