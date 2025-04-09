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
        public decimal CurrentBalance { get; set; }

        public ATMRequest(string transactionType, decimal requestAmount, decimal currentBalance)
        {
            this.TransactionType = transactionType;
            this.RequestAmount = requestAmount;
            this.CurrentBalance = currentBalance;
        }
    }
}
