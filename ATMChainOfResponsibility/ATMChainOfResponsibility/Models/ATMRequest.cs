using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMChainOfResponsibility.Models
{
    public class ATMRequest
    {
        public string TransactionType { get; set; }
        public decimal RequestAmount { get; set; }
        public Client Client { get; set; }

        public ATMRequest(string transactionType, decimal requestAmount, Client client)
        {
            this.TransactionType = transactionType;
            this.RequestAmount = requestAmount;
            this.Client = client;
        }
    }
}
